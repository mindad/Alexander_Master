using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Box : Product
    {
        protected int size;

        public Box(int size)
        {
            this.size = size;
        }
    }
}