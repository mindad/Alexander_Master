import pyodbc
import logging
import sys
import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
import statsmodels.api as sm
from datetime import datetime,date
from sklearn.linear_model import LinearRegression
from sklearn import preprocessing
from sklearn.metrics import mean_squared_error, r2_score


def Create_Regression(Waste_data_arr, col_list):
    
    try:
        df = pd.DataFrame(data=Waste_data_arr, columns=col_list) 
    except:
        logging.warning("No Values To Show")
        return

    # define input and output features
    X = df.drop(['waste_precent', 'batch_id'], axis=1)
    y = df.waste_precent

    regression = LinearRegression()  # define our model
    regression.fit(X,y)              # Fit our linear model    

    results_dict = CreateCoefDict(regression.coef_ , X)
      
    Get_P_values(y, X, results_dict)

    return results_dict


def CreateCoefDict(coef , X):
    coef_dict = {}
    for coef, feat in zip(coef , X):
        coef_dict[feat] = coef

    print("\ncoef_dict:\n")

    for key,val in coef_dict.items():
        coef_dict[key] = round(abs(val),5)

    results_dict = dict()
    counter = 0
    for key,val in sorted(coef_dict.items(), key = lambda kv:(kv[1],kv[0]), reverse=True):
        print(key, val)  
        if counter < 3:
            results_dict.setdefault(key, {})['coef'] = val
            counter += 1
    
    logging.info('Calculated coefficents')
    return results_dict


def Get_P_values(y, X, results_dict):
    if len(X) < 8:
        logging.warning('Unable to calculate P-values on less then 8 values')
        for key in results_dict.keys():
            results_dict.setdefault(key, {})['p_value'] = 0
        return

    mod = sm.OLS(y, X)
    fii = mod.fit()
    p_values = fii.summary2().tables[1]['P>|t|']

    print("\nP Values that are lower then significance level:\n")
    for key, value in p_values.items():
        if value < 0.05:
            print(key, round(value, 5))
            results_dict.setdefault(key, {})['p_value'] = value
        else:
            results_dict.setdefault(key, {})['p_value'] = 0
    logging.info('Calculated P-values')


def InsertResults(results, cursor, connection, start_period, end_period):
    st = ''
    for key,val in results.items():
        st += f"""'{key}',{val['coef']},{val['p_value']},"""

    try:
        cursor.execute(f"""INSERT INTO waste_2020 
        ([start_period],[end_period],[sig1],[coef1],[p_value1],[sig2],[coef2],[p_value2],[sig3],[coef3],[p_value3])
        Values
        ('{start_period}','{end_period}',{st[:-1]})
        """)

        connection.commit()

    except ValueError:
        logging.critical('Unable To Insert To Table !!!')
    
    
def GetTimePeriods():
    try:
        date_time_str = input("Insert START date in the format: yyyy-MM-dd\n")
        start_period = datetime.strptime(date_time_str, '%Y-%m-%d')
        date_time_str = input("Insert END date in the format: yyyy-MM-dd\n")
        end_period = datetime.strptime(date_time_str, '%Y-%m-%d')
        logging.info(f'start_period: {start_period.date()} end_period:{end_period.date()}')
    except ValueError:
        logging.warning('Time periods are invalid')
        return
    return start_period,end_period


def main():
    #logger_name = f'{str(date.today())}.log' # filename=logger_name, ## remove stream if you use this
    logging.basicConfig(format='%(asctime)s %(levelname)s: %(message)s',datefmt='%H:%M:%S',stream=sys.stdout, filemode='w', level=logging.DEBUG)

    start_period,end_period = GetTimePeriods()

    UID = input("Please Insert User ID for SQL DB\n")
    PWD = input("Please Insert Password for SQL DB\n")

    if UID and PWD:
        try:
            connection = pyodbc.connect("Driver={SQL Server Native Client 11.0};"
                            "Server=82.166.189.73;" ## IP of media.ruppin.ac.il 
                            "Database=igroup9_test1;"
                            "Trusted_Connection=no;"
                            f"UID={UID};"
                            f"PWD={PWD};"
                            )
            logging.info('Connected to server')
        except:
            logging.warning('Unable to connect to server')
            return

        cursor = connection.cursor()
    try: 
        cursor.execute(
        f"""SELECT batch_id,tank,wort_volume,yeast_cycle,co2_vol,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_temp,Purge_temp,weight,Num_Of_Buckets,pressure_change,pressure_tank,tank_temperature,waste_precent
        FROM (
        SELECT distinct Batch_2020.batch_id,Batch_2020.date,tank,wort_volume,yeast_cycle,co2_vol,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,
                Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_2020.temperature as Harvest_temp,Purge_2020.temperature as Purge_temp,weight,
                Num_Of_Buckets,pressure_change,pressure_tank,tank_temperature,waste_precent,
                Row_number() OVER(PARTITION BY Batch_2020.batch_id ORDER BY Batch_2020.date) rn
                FROM Batch_2020 join BatchAfterProd_2020 on Batch_2020.batch_id=BatchAfterProd_2020.batch_id join BatchAtProd_2020 on Batch_2020.batch_id=BatchAtProd_2020.batch_id join SampleDetails_2020 on Batch_2020.batch_id=SampleDetails_2020.batch_id join 
                Harvest_2020 on Harvest_2020.batch_id=Batch_2020.batch_id join Purge_2020 on Purge_2020.batch_id=Batch_2020.batch_id join Fermantation_2020 on Fermantation_2020.batch_id=Batch_2020.batch_id
                WHERE Batch_2020.date BETWEEN '{start_period.date()}' AND '{end_period.date()}'
        ) AS tbl 
        WHERE  rn = 1"""
        )
        logging.info('Query Success')
    except:
        logging.critical('Unable To Run Query')
        return

    # Create a list of coloumns
    col_list = [col[0] for col in cursor.description]

    Waste_data_arr = []
    # Create Dataset
    for row in cursor:
        Waste_data_arr.append(row)
    logging.info('Created Waste Array For Regression')

    highest_coef = Create_Regression(np.asarray(Waste_data_arr) , col_list) # np.asarray == tuple to array
    
    InsertResults(highest_coef, cursor, connection, start_period.date(), end_period.date())


if __name__ == "__main__":
    main()






### NO DATES:

# SELECT distinct Batch_2020.batch_id,tank,wort_volume,yeast_cycle,co2_vol,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,
# Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_2020.temperature as Harvest_temp,Purge_2020.temperature as Purge_temp,weight,Num_Of_Buckets,pressure_change,pressure_tank,tank_temperature,waste_precent
# FROM Batch_2020 join BatchAfterProd_2020 on Batch_2020.batch_id=BatchAfterProd_2020.batch_id join BatchAtProd_2020 on Batch_2020.batch_id=BatchAtProd_2020.batch_id join SampleDetails_2020 on Batch_2020.batch_id=SampleDetails_2020.batch_id join 
# Harvest_2020 on Harvest_2020.batch_id=Batch_2020.batch_id join Purge_2020 on Purge_2020.batch_id=Batch_2020.batch_id join Fermantation_2020 on Fermantation_2020.batch_id=Batch_2020.batch_id
# WHERE Batch_2020.date between '{}' AND '{}'

# SELECT distinct Batch_2020.batch_id,tank,wort_volume,yeast_cycle,co2_vol,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,keg_20_amount,keg_30_amount,bottles_qty,waste_litter,waste_precent,waste_precent,
# Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_2020.temperature as Harvest_temp,Purge_2020.temperature as Purge_temp,weight,Num_Of_Buckets,pressure_change,pressure_tank,tank_temperature
# FROM Batch_2020 join BatchAfterProd_2020 on Batch_2020.batch_id=BatchAfterProd_2020.batch_id join BatchAtProd_2020 on Batch_2020.batch_id=BatchAtProd_2020.batch_id join SampleDetails_2020 on Batch_2020.batch_id=SampleDetails_2020.batch_id join 
# Harvest_2020 on Harvest_2020.batch_id=Batch_2020.batch_id join Purge_2020 on Purge_2020.batch_id=Batch_2020.batch_id join Fermantation_2020 on Fermantation_2020.batch_id=Batch_2020.batch_id
# WHERE Batch_2020.date between '2020-04-03' and '2020-04-03'

### WITH DATES:

# select distinct Batch_2020.batch_id,Batch_2020.date,tank,wort_volume,beer_type,cast_volume,yeast_cycle,co2_vol,pitch_time,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,
#     keg_20_amount,keg_30_amount,bottles_qty,waste_litter,waste_precent,purge_amount,prod_waste,harvest_amount,beer_req_litter,filling_hose,tank_leftover,
#     SampleDetails_2020.date as sample_date,Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_2020.date as Harvest_date,Harvest_2020.temperature as Harvest_temp,timeForTapTwo,total_time,Purge_2020.date as Purge_date,Purge_2020.temperature as Purge_temp,weight,Num_Of_Buckets,Fermantation_2020.date as Fermantation_date,pressure_change,pressure_tank,tank_temperature
#     from Batch_2020 join BatchAfterProd_2020 on Batch_2020.batch_id=BatchAfterProd_2020.batch_id join BatchAtProd_2020 on Batch_2020.batch_id=BatchAtProd_2020.batch_id join SampleDetails_2020 on Batch_2020.batch_id=SampleDetails_2020.batch_id join 
#     Harvest_2020 on Harvest_2020.batch_id=Batch_2020.batch_id join Purge_2020 on Purge_2020.batch_id=Batch_2020.batch_id join Fermantation_2020 on Fermantation_2020.batch_id=Batch_2020.batch_id;


### https://scikit-learn.org/stable/modules/generated/sklearn.linear_model.LinearRegression.html

# f"""
# SELECT batch_id,tank,wort_volume,yeast_cycle,co2_vol,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_temp,Purge_temp,weight,Num_Of_Buckets,pressure_change,pressure_tank,tank_temperature,waste_precent
# FROM (
# SELECT distinct Batch_2020.batch_id,Batch_2020.date,tank,wort_volume,yeast_cycle,co2_vol,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,
#         Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_2020.temperature as Harvest_temp,Purge_2020.temperature as Purge_temp,weight,
#         Num_Of_Buckets,pressure_change,pressure_tank,tank_temperature,waste_precent,
# 		Row_number() OVER(PARTITION BY Batch_2020.batch_id ORDER BY Batch_2020.date) rn
#         FROM Batch_2020 join BatchAfterProd_2020 on Batch_2020.batch_id=BatchAfterProd_2020.batch_id join BatchAtProd_2020 on Batch_2020.batch_id=BatchAtProd_2020.batch_id join SampleDetails_2020 on Batch_2020.batch_id=SampleDetails_2020.batch_id join 
#         Harvest_2020 on Harvest_2020.batch_id=Batch_2020.batch_id join Purge_2020 on Purge_2020.batch_id=Batch_2020.batch_id join Fermantation_2020 on Fermantation_2020.batch_id=Batch_2020.batch_id
#         WHERE Batch_2020.date BETWEEN '{}' AND '{}'
# ) AS tbl 
# WHERE  rn = 1
# """