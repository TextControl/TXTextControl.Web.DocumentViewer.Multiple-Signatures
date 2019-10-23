using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tx_viewer_sign_multiple.Models;

namespace tx_viewer_sign_multiple.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Overview()
        {
            List<DocumentFlow> DocumentFlows = new List<DocumentFlow>();

            // read all envelope structures
            foreach (string info in Directory.EnumerateDirectories(Server.MapPath("~/App_Data/documentflows/")))
            {
                string jsonFlow = System.IO.File.ReadAllText(info + "/documentflow.json");
                DocumentFlow documentflow = Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentFlow>(jsonFlow);

                DocumentFlows.Add(documentflow);
            }

            // return view with envelope structures
            return View(DocumentFlows);
        }

        public ActionResult Thanks()
        {
            return View();
        }

        public ActionResult Sign(string envelopeId, int signerId)
        {
            // read the specific envelope structure
            string jsonFlow = System.IO.File.ReadAllText(
                Server.MapPath("~/App_Data/documentflows/" + envelopeId + "/documentflow.json"));

            DocumentFlow documentflow = Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentFlow>(jsonFlow);
            Signer signer = documentflow.Signers.Find(x => x.Id == signerId);

            SignProcess process = new SignProcess()
            {
                EnvelopeId = documentflow.EnvelopeId,
                Owner = documentflow.Owner,
                Signer = signer
            };

            // return the template or the current signed document
            if (System.IO.File.Exists(
                Server.MapPath("~/App_Data/documentflows/" + envelopeId + "/signed.tx")) == true)
                process.Template = "signed.tx";
            else
                process.Template = documentflow.Template;

            return View(process);
        }
    }
}