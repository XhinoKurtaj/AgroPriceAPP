using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AgroPrice.Web.Infrastructure.TagHelpers
{
    [HtmlTargetElement("modal")]
    public class ModalTagHelper : TagHelper
    {
        [HtmlAttributeName("id")]
        public string Id { get; set; }

        [HtmlAttributeName("title")]
        public string Title { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var body = (await output.GetChildContentAsync()).GetContent();
            body = body.Trim();

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", "modal fade");
            output.Attributes.SetAttribute("id", Id);

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"<div class='modal-dialog modal-lg' role='document'>");
            stringBuilder.AppendLine($"\t<div class='modal-content'>");
            stringBuilder.AppendLine($"\t\t<div class='card card-plain'>");
            stringBuilder.AppendLine($"\t\t\t<div class='modal-header'>");
            stringBuilder.AppendLine($"\t\t\t\t<div class='card-header card-header-primary text-center' style='width: 100%'>");
            stringBuilder.AppendLine($"\t\t\t\t\t<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>");
            stringBuilder.AppendLine($"\t\t\t\t\t\t<i class='material-icons'>Mbylle</i>");
            stringBuilder.AppendLine($"\t\t\t\t\t</button>");
            stringBuilder.AppendLine($"\t\t\t\t\t<h4 class='card-title'>{Title}</h4>");
            stringBuilder.AppendLine($"\t\t\t\t</div>");
            stringBuilder.AppendLine($"\t\t\t</div>");
            stringBuilder.AppendLine($"\t\t\t<div id='{Id}Body' class='modal-body'>");
            stringBuilder.AppendLine($"\t\t\t{body}");
            stringBuilder.AppendLine($"\t\t\t</div>");
            stringBuilder.AppendLine($"\t\t</div>");
            stringBuilder.AppendLine($"\t</div>");
            stringBuilder.AppendLine($"</div>");

            output.Content.SetHtmlContent(stringBuilder.ToString());

            //await base.ProcessAsync(context, output);
        }
    }
}
