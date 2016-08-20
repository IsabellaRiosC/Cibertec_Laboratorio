using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDeveloper.Helpers
{
    public static class CustomHelper
    {
        public static IHtmlString GetDate(this HtmlHelper helper,string text)
        {
            //"<h1>@demo - @DateTime.Now.ToShortDateString()</h1>";
            //$=>string format
            return new HtmlString($"<h1>{text}-{DateTime.Now.ToShortDateString()}</h1>");
        }
    }
}