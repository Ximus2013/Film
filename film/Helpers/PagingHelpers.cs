﻿using film.ViewModels;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

public static class PagingHelpers
{
    public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
    {
        var result = new StringBuilder();
        for (var i = 1; i <= pageInfo.TotalPages; i++)
        {
            var tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(i));
            tag.InnerHtml = i.ToString();
            // если текущая страница, то выделяем ее,
            // например, добавляя класс
            if (i == pageInfo.PageNumber)
            {
                tag.AddCssClass("selected");
                tag.AddCssClass("btn-primary");
            }
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());
        }
        return MvcHtmlString.Create(result.ToString());
    }
}