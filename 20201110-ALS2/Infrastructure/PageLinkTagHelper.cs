using System.Collections.Generic;
using _20201110_ALS2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace _20201110_ALS2.Infrastructure {
  [HtmlTargetElement("div", Attributes = "page-model")]
  public class PageLinkTagHelper : TagHelper {
    private IUrlHelperFactory urlHelperFactory;

    public PageLinkTagHelper(IUrlHelperFactory helperFactory) {
      urlHelperFactory = helperFactory;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    public PagingInfo PageModel { get; set; }

    public string PageAction { get; set; }

    [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
    public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

    public override void Process(TagHelperContext context, TagHelperOutput output) {
      IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
      TagBuilder result = new TagBuilder("div");

      TagBuilder tag = new TagBuilder("a");
      PageUrlValues["slide"] = 1;
      tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);

      tag.InnerHtml.Append(1.ToString());
      result.InnerHtml.AppendHtml(tag);

      output.Content.AppendHtml(result.InnerHtml);
    }

  }
}
