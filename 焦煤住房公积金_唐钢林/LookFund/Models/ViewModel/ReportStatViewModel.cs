using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LookFund.Models.ViewModel
{
    public class ReportStatViewModel
    {
        private string statDate = "0";
        private string endDate = "999912";
        public string StatDate
        {
            get
            {
                return statDate;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    statDate = value.Replace("-", "");
                }
            }
        }

        public string EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    endDate = value.Replace("-", "");
                }
            }
        }
        public string strUnit
        {
            get;
            set;
        }
    }
}