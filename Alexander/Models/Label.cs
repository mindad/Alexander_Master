using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Label
    {
        protected string beerType;
        protected string labelType;

        public Label()
        {
        }

        protected string BeerType { get => beerType; set => beerType = value; }
        protected string LabelType { get => labelType; set => labelType = value; }
    }
}