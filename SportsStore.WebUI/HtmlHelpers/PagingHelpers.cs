using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder linkTag = new TagBuilder("a"); //Construct an <a> tag
                linkTag.MergeAttribute("href", pageUrl(i));
                linkTag.InnerHtml = i.ToString();

                TagBuilder listitemTag = new TagBuilder("li");
                listitemTag.InnerHtml = linkTag.ToString();
                if (i == pagingInfo.CurrentPage)
                    listitemTag.AddCssClass("active");
                result.Append(listitemTag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}