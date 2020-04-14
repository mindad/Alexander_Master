using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;


namespace Alexander.Controllers
{
    public class RecipeController : ApiController
    {
        public List<Recipe> Get()
        {
            Recipe recipe = new Recipe();
            return recipe.get_Recipes();
        }

        public HttpResponseMessage Post([FromBody]Recipe recipe)
        {
            int numEffected = 0;
            try
            {
                numEffected = recipe.CreateRecipe();

                if (numEffected > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, numEffected);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        //[HttpPut]
        //public HttpResponseMessage Put([FromBody]Recipe recipe)
        //{
        //    int numEffected = 0;

        //    try
        //    {
        //        numEffected = recipe.Update();

        //        if (numEffected > 0)
        //            return Request.CreateResponse(HttpStatusCode.OK, numEffected);
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }

        //}


        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]string beerType) // row = row number in DB
        {
            int numEffected = 0;
            Recipe recipe = new Recipe();

            try
            {
                numEffected = recipe.delete_line(beerType);

                if (numEffected > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, numEffected);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
