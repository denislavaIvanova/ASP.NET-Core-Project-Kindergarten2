#pragma checksum "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\ECAs\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "32e9b64057e78a46661d35b78aeb77b4c60c0e9c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ECAs_Details), @"mvc.1.0.view", @"/Views/ECAs/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Models.Teachers;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Models.ECAs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Models.Menus;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Models.Trips;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Models.Childs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Models.Home;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Models.Parents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Infrastructure;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Services.Childs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Services.Parents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Services.ECAs;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Services.Menus;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Services.Trips;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\_ViewImports.cshtml"
using Kindergarten2.Services.Teachers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"32e9b64057e78a46661d35b78aeb77b4c60c0e9c", @"/Views/ECAs/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b82468827175526b01e274513e7c06c71af89f83", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ECAs_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ECAServiceModel>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\ECAs\Details.cshtml"
  
	ViewBag.Title = "Extracurricular Activity Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n\t<div class=\"row\">\r\n\t\t<div class=\"col-md-4\">\r\n\t\t\t<div class=\"card mb-3\">\r\n\t\t\t\t<img class=\"card-img-top\"");
            BeginWriteAttribute("src", " src=\"", 217, "\"", 238, 1);
#nullable restore
#line 11 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\ECAs\Details.cshtml"
WriteAttributeValue("", 223, Model.ImageUrl, 223, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 239, "\"", 277, 3);
#nullable restore
#line 11 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\ECAs\Details.cshtml"
WriteAttributeValue("", 245, Model.Title, 245, 12, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 257, "-", 258, 2, true);
#nullable restore
#line 11 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\ECAs\Details.cshtml"
WriteAttributeValue(" ", 259, Model.MonthlyFee, 260, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n\t\t\t\t<div class=\"card-body text-center\">\r\n\t\t\t\t\t<h5 class=\"card-title text-center\">");
#nullable restore
#line 13 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\ECAs\Details.cshtml"
                                                  Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" for only ");
#nullable restore
#line 13 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\ECAs\Details.cshtml"
                                                                        Write(Model.MonthlyFee);

#line default
#line hidden
#nullable disable
            WriteLiteral(" EUR per month.</h5>\r\n\t\t\t\t\t<p class=\"text-center\">");
#nullable restore
#line 14 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\ECAs\Details.cshtml"
                                      Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\t\t\t\t\t<a href=\"/ECAs/All\" class=\"btn btn-primary\">Go back</a>\r\n\r\n\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\r\n\t\t</div>\r\n\t</div>\r\n</div>\r\n\r\n\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ECAServiceModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
