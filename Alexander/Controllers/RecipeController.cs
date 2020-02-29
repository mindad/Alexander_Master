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
    }
}
