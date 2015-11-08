using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Novacode;
using prospekt.tel.Models;

namespace prospekt.tel.Controllers.Api
{
    [Authorize]
    public class PrintOrdersController : ApiController
    {
        private psEnt db = new psEnt();

        public IHttpActionResult Get()
        {
            var cPath = System.Configuration.ConfigurationManager.AppSettings["TemplatesPath"].ToString();
            using (DocX d = DocX.Load(cPath + @"ordertmpl.docx"))
            {
                d.ReplaceText("buyerFIO", "Сулимова Олеся Александровна", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.ExactMatch);
                d.ReplaceText("sellerpassser", "93 03", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.ExactMatch);
                d.ReplaceText("sellerpasnum", "222854", false, System.Text.RegularExpressions.RegexOptions.None, null, null, MatchFormattingOptions.ExactMatch);
                d.SaveAs(cPath + @"ordertmpl_2.docx");
                return Ok();
            }
        }
    }
}
