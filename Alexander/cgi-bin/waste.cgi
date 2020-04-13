#!/usr/bin/env python
# -*- coding: UTF-8 -*-

import pyodbc
import pandas as pd
import numpy as np
import seaborn as sns
import matplotlib.pyplot as plt
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error, r2_score


def main():
    try:
        con = pyodbc.connect("Driver={SQL Server Native Client 11.0};"
                        "Server=82.166.189.73;" ## IP of media.ruppin.ac.il 
                        "Database=igroup9_test1;"
                        "Trusted_Connection=no;"
                        "UID=igroup9;"
                        "PWD=igroup9_86098;"
                        )
        print('[+] Connected to server')
    except:
        print('[+] Unable to connect to server')

    cursor = con.cursor()
    try:
         cursor.execute('''
        select distinct Batch_2020.batch_id,tank,wort_volume,cast_volume,yeast_cycle,co2_vol,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,
        keg_20_amount,keg_30_amount,bottles_qty,waste_litter,waste_precent,purge_amount,prod_waste,harvest_amount,beer_req_litter,filling_hose,tank_leftover,
        Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_2020.temperature as Harvest_temp,Purge_2020.temperature as Purge_temp,weight,Num_Of_Buckets,pressure_change,pressure_tank,tank_temperature
        from Batch_2020 join BatchAfterProd_2020 on Batch_2020.batch_id=BatchAfterProd_2020.batch_id join BatchAtProd_2020 on Batch_2020.batch_id=BatchAtProd_2020.batch_id join SampleDetails_2020 on Batch_2020.batch_id=SampleDetails_2020.batch_id join 
        Harvest_2020 on Harvest_2020.batch_id=Batch_2020.batch_id join Purge_2020 on Purge_2020.batch_id=Batch_2020.batch_id join Fermantation_2020 on Fermantation_2020.batch_id=Batch_2020.batch_id;
        ''')
    except:
        print('[+] Unable to run query')
   
    Waste_data_arr = []

    col_list = [col[0] for col in cursor.description]

    #print(col_list) ## list of coloumns
    # for col in cursor.description: # list of columns and their types
    #     print(col[0],col[1])

    for row in cursor:
        #print('row = %r' % (row,))
        Waste_data_arr.append(row)
        
        # print(row.batch_id) # how to address a row value based on col name
        # print(row.bottles_qty)

        print(Waste_data_arr)



    Create_Regression(np.asarray(Waste_data_arr) , col_list) # np.asarray == tuple to array
    

def Create_Regression(Waste_data_arr, col_list):
    ### https://scikit-learn.org/stable/modules/generated/sklearn.linear_model.LinearRegression.html
    
    try:
        df = pd.DataFrame(data=Waste_data_arr, columns=col_list) 
    except:
        print("No Values To Show")
        return
    
    
    # print(df.head())
    # print(df.columns)
    # print(df.dtypes)
    #print(df.describe)


    # define input and output features
    
    X = df.drop(['waste_precent', 'batch_id', 'tank'], axis=1)   #df[feature_cols]
    y = df.waste_precent

    regression = LinearRegression()  # define our model
    regression.fit(X,y)              # Fit our linear model    

    print("coefficients are:", regression.coef_) # array of coefficients



if __name__ == "__main__":
    main()


### NO DATES:

# select distinct Batch_2020.batch_id,tank,wort_volume,beer_type,cast_volume,yeast_cycle,co2_vol,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,
# keg_20_amount,keg_30_amount,bottles_qty,waste_litter,waste_precent,purge_amount,prod_waste,harvest_amount,beer_req_litter,filling_hose,tank_leftover,
# Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_2020.temperature as Harvest_temp,Purge_2020.temperature as Purge_temp,weight,Num_Of_Buckets,pressure_change,pressure_tank,tank_temperature
# from Batch_2020 join BatchAfterProd_2020 on Batch_2020.batch_id=BatchAfterProd_2020.batch_id join BatchAtProd_2020 on Batch_2020.batch_id=BatchAtProd_2020.batch_id join SampleDetails_2020 on Batch_2020.batch_id=SampleDetails_2020.batch_id join 
# Harvest_2020 on Harvest_2020.batch_id=Batch_2020.batch_id join Purge_2020 on Purge_2020.batch_id=Batch_2020.batch_id join Fermantation_2020 on Fermantation_2020.batch_id=Batch_2020.batch_id;

### WITH DATES:

# select distinct Batch_2020.batch_id,Batch_2020.date,tank,wort_volume,beer_type,cast_volume,yeast_cycle,co2_vol,pitch_time,OG,FG,Pitching_Rate,Temp_Tank,Set_Temp,
#     keg_20_amount,keg_30_amount,bottles_qty,waste_litter,waste_precent,purge_amount,prod_waste,harvest_amount,beer_req_litter,filling_hose,tank_leftover,
#     SampleDetails_2020.date as sample_date,Tank_temp,Sample_Temp,Rate,Gravity,ph,Harvest_2020.date as Harvest_date,Harvest_2020.temperature as Harvest_temp,timeForTapTwo,total_time,Purge_2020.date as Purge_date,Purge_2020.temperature as Purge_temp,weight,Num_Of_Buckets,Fermantation_2020.date as Fermantation_date,pressure_change,pressure_tank,tank_temperature
#     from Batch_2020 join BatchAfterProd_2020 on Batch_2020.batch_id=BatchAfterProd_2020.batch_id join BatchAtProd_2020 on Batch_2020.batch_id=BatchAtProd_2020.batch_id join SampleDetails_2020 on Batch_2020.batch_id=SampleDetails_2020.batch_id join 
#     Harvest_2020 on Harvest_2020.batch_id=Batch_2020.batch_id join Purge_2020 on Purge_2020.batch_id=Batch_2020.batch_id join Fermantation_2020 on Fermantation_2020.batch_id=Batch_2020.batch_id;