﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMModels.Create
{
    public class CreateHistory
    {
        //HistoryID & CreatedDateUTC are set at the service layer. There is only a Create and Read for the History Table
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public string Table { get; set; }
        public int? IntID { get; set; }
        public string Change { get; set; }
    }
}