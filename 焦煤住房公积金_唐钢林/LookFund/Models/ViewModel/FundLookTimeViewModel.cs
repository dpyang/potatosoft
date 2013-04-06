using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using TglFirst.Core;
using LookFund.Dao.FundInfo;
using System.ComponentModel.DataAnnotations;

namespace LookFund.Models.ViewModel
{
    public class FundLookTimeViewModel
    {
        [Required(ErrorMessage = "开始时间不能为空。")]
        public string BegionTime
        {
            get;
            set;
        }

        public string EndTime
        {
            get;
            set;
        }

        public bool ViliFundDate(ModelStateDictionary state)
        {
            return true;
        }

        public string GetWhere()
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(BegionTime))
            {
                strWhere += " and C7>='" + BegionTime + "'";
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                strWhere += " and C7<='" + EndTime + "'";
            }
            return strWhere;
        }
    }
}