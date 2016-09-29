using System.Linq;
using System.Text;
using System.Web.Mvc;
using System;
namespace Weiz.TaskManager.HouTai_New
{
    public static class FPage
    {
        public static IQueryable<T> GetPageList<T>(IOrderedQueryable<T> List, int PageIndex, int PageSize)
        {
            int PageCount = GetPageCount(PageSize, List.Count());
            PageIndex = CheckPageIndex(PageIndex, PageCount);
            return List.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        }

        public static int GetPageCount(int PageSize, int recordCount)
        {
            int PageCount = recordCount % PageSize == 0 ? recordCount / PageSize : recordCount / PageSize + 1;
            if (PageCount < 1) PageCount = 1;
            return PageCount;
        }

        public static int CheckPageIndex(int PageIndex, int PageCount)
        {
            if (PageIndex > PageCount) PageIndex = PageCount;
            if (PageIndex < 1) PageIndex = 1;
            return PageIndex;
        }

        public enum FPageMode { Normal, Numeric, GroupNumeric }

        public static MvcHtmlString ShowFPage(this HtmlHelper Html, string urlFormat, int PageIndex, int PageSize, int recordCount, FPageMode Mode)
        {
            urlFormat = urlFormat.Replace("%7B0%7D", "{0}");
            int PageCount = GetPageCount(PageSize, recordCount);

            StringBuilder TempHtml = new StringBuilder();
            //TempHtml.AppendFormat("总共&nbsp;[<font>{0}</font>]&nbsp;条记录&nbsp;&nbsp;&nbsp;共&nbsp;[<font>{1}</font>]&nbsp;页&nbsp;&nbsp;&nbsp;当前第&nbsp;[<font>{2}</font>]&nbsp;页&nbsp;&nbsp;&nbsp;", recordCount, PageCount, PageIndex);
            if (PageIndex == 1)
            {
                TempHtml.Append("<a href=\"javascript:void(0)\" class=\"pgEmpty\">首页</a>&nbsp;<a href=\"javascript:void(0)\" class=\"pgEmpty\">上一页</a>&nbsp;&nbsp;");
            }
            else
            {
                TempHtml.AppendFormat("<a href=\"{0}\"  class=\"pgNext\">首页</a>&nbsp;&nbsp;", string.Format(urlFormat, 1))
                    .AppendFormat("<a href=\"{0}\" class=\"pgNext\">上一页</a>&nbsp;&nbsp;", string.Format(urlFormat, PageIndex - 1));
            }
            // 数字分页
            switch (Mode)
            {
                case FPageMode.Numeric:
                    TempHtml.Append(GetNumericPage(urlFormat, PageIndex, PageSize, PageCount));
                    break;
                case FPageMode.GroupNumeric:
                    TempHtml.Append(GetGroupNumericPage(urlFormat, PageIndex, PageSize, PageCount));
                    break;
            }

            if (PageIndex == PageCount)
            {
                TempHtml.Append("<a href=\"javascript:void(0)\" class=\"pgEmpty\">下一页</a>&nbsp;&nbsp;<a href=\"javascript:void(0)\" class=\"pgEmpty\">末页</a>");
            }
            else
            {
                TempHtml.AppendFormat("<a href=\"{0}\" class=\"pgNext\">下一页</a>&nbsp;&nbsp;", string.Format(urlFormat, PageIndex + 1))
                    .AppendFormat("<a href=\"{0}\" class=\"pgNext\">末页</a>", string.Format(urlFormat, PageCount));
            }

            return MvcHtmlString.Create(TempHtml.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="urlFormat"></param>
        /// <param name="PageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static MvcHtmlString ShowFPage(this HtmlHelper Html, string urlFormat, int PageIndex,int PageSize, int recordCount)
        {
            FPageMode Mode = FPageMode.Numeric;
            return ShowFPage(Html, urlFormat, PageIndex, PageSize, recordCount, Mode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Html"></param>
        /// <param name="urlFormat"></param>
        /// <param name="PageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public static MvcHtmlString ShowFPage(this HtmlHelper Html, string urlFormat, int PageIndex, int recordCount)
        {
            int PageSize = 10;
            
            FPageMode Mode = FPageMode.Numeric;
            return ShowFPage(Html, urlFormat, PageIndex, PageSize,recordCount, Mode);
        }

        /// <summary>
        /// 分组数字分页
        /// </summary>
        /// <param name="urlFormat"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static string GetGroupNumericPage(string urlFormat, int pageIndex, int pageSize, int pageCount)
        {
            int GroupChildCount = 5; // 分组显示个数
            int DGroup = pageIndex / GroupChildCount; //当前组
            int GroupCount = pageCount / GroupChildCount;      //组数

            //如果正好是当前组最后一页 不进入下一组
            if (pageIndex % GroupChildCount == 0) DGroup--;

            //当前组数量
            int GroupSpan = (DGroup == GroupCount) ? pageCount % GroupChildCount : GroupChildCount;

            StringBuilder TempHtml = new StringBuilder();
            for (int i = DGroup * GroupChildCount + 1; i <= DGroup * GroupChildCount + GroupSpan; i++)
            {
                if (i == pageIndex)
                    TempHtml.AppendFormat("<a href=\"javascript:void(0)\" class=\"pgCurrent\"> {0}</a>&nbsp;", i);
                else
                    TempHtml.AppendFormat("<a href=\"{0}\" class=\"pgNext\">{1}</a>&nbsp;", string.Format(urlFormat, i), i);
            }
            return TempHtml.ToString();
        }

        /// <summary>
        /// 数字分页
        /// </summary>
        /// <param name="urlFormat"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static string GetNumericPage(string urlFormat, int pageIndex, int pageSize, int pageCount)
        {
            int SpanNum =10;//显示个数

            int BeginNum =0;
            if (pageIndex < 7)
                BeginNum = 1;
            else
                BeginNum = pageIndex - 5;

            if (BeginNum < 1) BeginNum = 1;
            int EndNum = BeginNum + (SpanNum - 1);
            if (EndNum > pageCount)
            {
                EndNum = pageCount;
                BeginNum = pageCount - (SpanNum - 1);
                if (BeginNum < 1) BeginNum = 1;
            }


            StringBuilder TempHtml = new StringBuilder();
            for (int i = BeginNum; i <= EndNum; i++)
            {
                if (i == pageIndex)
                    TempHtml.AppendFormat("<a href=\"javascript:void(0)\" class=\"pgCurrent\"> {0}</a>&nbsp;", i);
                else
                    TempHtml.AppendFormat("<a href=\"{0}\" class=\"pgNext\">{1}</a>&nbsp;", string.Format(urlFormat, i), i);
            }
            return TempHtml.ToString();
        }
    }
}