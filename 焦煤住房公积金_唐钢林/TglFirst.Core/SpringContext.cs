using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;

namespace TglFirst.Core
{
    public class SpringContext
    {
        private static IApplicationContext context;

        public SpringContext()
        {
            context = new XmlApplicationContext(new[]{
                @"assembly://TglFirst.Core/TglFirst.Core.Config/Application-Context.xml",
                @"assembly://LookFund/LookFund.Config/Application-Context.xml"
            });
        }
        
        public SpringContext(string rootFild)
        {
            context = new XmlApplicationContext(new []{
                @"assembly://TglFirst.Core/TglFirst.Core.Config/Application-Context.xml",
               rootFild
            });
        }

        public static IApplicationContext Context
        {
            get
            {
                return context;
            }
        }
    }
}
