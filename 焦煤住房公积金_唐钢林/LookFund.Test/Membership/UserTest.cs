using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TglFirst.Core;
using LookFund.Dao.Membership;
using LookFund.Models.Membership;
using TglFirst.Core.Model.Example;
using LookFund.Dao.FundInfo;
using System.Data;
using NHibernate;

namespace LookFund.Test.Membership
{
    public class UserTest
    {
        [Test]
        public void CreateEmploye()
        {
            SpringContext con = new SpringContext();
            var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            var menu = SpringContext.Context.GetObject("dao.FunctionMenu") as IFunctionMenu;
            var list = new List<FunctionMenu>();
            //list.Add(menu.Query("LevelCode", "1101").First());
            //list.Add(menu.Query("LevelCode", "110101").First());
            //list.Add(menu.Query("LevelCode", "110102").First());
            //list.Add(menu.Query("LevelCode", "1102").First());
            //list.Add(menu.Query("LevelCode", "110202").First());
            //list.Add(menu.Query("LevelCode", "1106").First());
            //list.Add(menu.Query("LevelCode", "110601").First());
            //MEmploye model = new MEmploye()
            //{
            //    LoginName = "admin",
            //    LoginPwk = "5c28fe703df24357",
            //    ShowName = "管理员",
            //    FunctionList = list,
            //    Kind = 2,
            //    BelongCompany = Company.西山矿务局
            //};

            list.Add(menu.Query("LevelCode", "1104").First());
            list.Add(menu.Query("LevelCode", "110401").First());
            list.Add(menu.Query("LevelCode", "110402").First());
            list.Add(menu.Query("LevelCode", "1103").First());
            list.Add(menu.Query("LevelCode", "110302").First());
            list.Add(menu.Query("LevelCode", "1106").First());
            list.Add(menu.Query("LevelCode", "110601").First());
            list.Add(menu.Query("LevelCode", "110602").First());
            MEmploye model = new MEmploye()
            {
                LoginName = "Administrator",
                LoginPwk = "5c28fe703df24357",
                ShowName = "管理员",
                FunctionList = list,
                EmpKind = "11"
            };
            dao.Save(model);            
        }

        [Test]
        public void CreateOperation()
        {
            SpringContext con = new SpringContext();

            var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            //var model = dao.Get(new Guid("2A7070BD-7430-440F-8EE0-9F0B013AE48E"));

            var list = dao.Query("LoginName", "210902196903020087");


           // NHibernateUtil.Initialize(model.CommentsList);                   
            //var list = model.FunctionList;
            //Console.Write(list.Count);

            //var menu = SpringContext.Context.GetObject("dao.FunctionMenu") as IFunctionMenu;
            //menu.TableTree(model);   


            //var dao = SpringContext.Context.GetObject("dao.FunctionMenu") as IFunctionMenu;


            //var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            //var model = dao.Query("LoginName", "tgl").First();
            //var model1 = dao.Get(model.Uid);
            //model1.LoginPwk = "1235";

            //dao.Update(model1);

            //var menu = SpringContext.Context.GetObject("dao.FunctionMenu") as IFunctionMenu;
            //string str = menu.TableTree();
            //Console.Write(str);

            //FunctionMenu model = new FunctionMenu()
            //{
            //    LevelCode="110401",
            //    FunctionName="山西焦煤住房公积金查询系统",
            //    NextNodeNumber=1,
            //    ActionName = "\"add\",\"htt\""
            //};
            //dao.Save(model);
        }

        [Test]
        public void UpdateType()
        {
            SpringContext con = new SpringContext();

            //var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            //var str= dao.DateMonth("E324ACCC-A960-4A2C-BFC4-73773F32C882");
            //Console.Write(str.Rows.Count);


            //dao.Exits("12312", "ddd");
            //dao.GetAll();
            //ReadXML readXml = ReadXML.getInstance();
            //string strSql = "select A11,A13 from [Sheet1$]";
            //ImportExcel excel = new ImportExcel();
            //DataTable dt = excel.ReadFile("E:\\Work\\JMElemsun\\LookFund\\Uploads\\20110613113210.xls", strSql);

            //var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            //dao.VerifySucced("397E3731-21F1-42FE-B518-4AD208902CFF");


            //var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            //bool b = dao.Exits("b68912a1-1ba1-4f81-9ef3-9f0600b53056");
            //Console.Write(b.ToString());

            //var model =  dao.BaseInfo("500234198606042847");
            //Console.Write(model.Rows.Count);
            //string htmlPage = string.Empty;
            //var list = dao.DateMonth(1, 20, out htmlPage);
            //foreach (var item in list)
            //{
            //    var cols = (object[])item;
            //    foreach (var text in cols)
            //    {
            //        Console.Write(text);
            //    }
            //}

            //ReadXML xml = ReadXML.getInstance();
            //Console.Write(xml.VerifyShowHead);


            //var empDao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            //var model = empDao.Get(new Guid("83AE3C17-B992-48AB-A386-9F08010B68F3"));
            //var dao = SpringContext.Context.GetObject("dao.FunctionMenu") as IFunctionMenu;
            //model.FunctionList = dao.AdminMenuList();
            //empDao.Update(model);
            //empDao.Delete(model);
            //empDao.GetAll();

            //MEmploye model = new MEmploye();
            //model.BelongCompany = Company.西山矿务局;

            //Console.WriteLine((int)model.BelongCompany);

            //var m = (Company)Enum.Parse(typeof(Company), "2");

            //string str = Enum.GetName(typeof(Company), 1);
            //Enum.
            //Console.WriteLine(m.ToString());

            //string[] str = Enum.GetNames(typeof(Company));
            //foreach (var item in str)
            //{
            //    Console.WriteLine(item + "," + (int)Enum.Parse(typeof(Company), item));
            //}
        }

        [Test]
        public void pageTest()
        {
            SpringContext con = new SpringContext();


            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            string PageHtml = string.Empty;
            var list = dao.GetList("1102", 1, 10, out PageHtml);


            //ReadXML readXml = ReadXML.getInstance();

            ////var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            ////var model = dao.Get(new Guid("2A7070BD-7430-440F-8EE0-9F0B013AE48E"));

            //var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            //string htmlPage = string.Empty;
            //var list = dao.DateMonth(1, 20, out htmlPage, "11");
        }
    }
}
