#pragma checksum "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\Childs\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4538d72dae9d7a7f6fd9101c35cd90c8f22b6448"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Childs_Edit), @"mvc.1.0.view", @"/Views/Childs/Edit.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4538d72dae9d7a7f6fd9101c35cd90c8f22b6448", @"/Views/Childs/Edit.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ee84c8812d602b54edccd404b38c4901c03895da", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Childs_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ChildFormModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ChildFormPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\Childs\Edit.cshtml"
  
	ViewBag.Title = "Edit Child";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4538d72dae9d7a7f6fd9101c35cd90c8f22b64485951", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#nullable restore
#line 7 "C:\Users\Bumba\Desktop\SoftUni\C#Web\ProjectKenov\Kindergarten\Kindergarten2\Views\Childs\Edit.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ChildFormModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
