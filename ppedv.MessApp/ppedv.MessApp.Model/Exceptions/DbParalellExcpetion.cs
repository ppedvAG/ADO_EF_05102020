using System;
using System.Collections.Generic;
using System.Text;

namespace ppedv.MessApp.Model.Exceptions
{
    public class DbParalellExcpetion : Exception
    {
        public Action DatabaseWins { get; set; }
        public Action UserWins { get; set; }


    }
}
