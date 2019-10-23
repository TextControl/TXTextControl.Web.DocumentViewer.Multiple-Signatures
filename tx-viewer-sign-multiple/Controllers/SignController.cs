using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tx_viewer_sign_multiple.Models;

namespace tx_viewer_sign_multiple.Controllers
{
    public class SignController : Controller
    {
        // This HTTP post method accepts the binary signed document and stores it as the
        // current signed document.
        // Additionally, the status is updated in the document flow json file.
        [HttpPost]
        public ActionResult SignDocument(
            string UniqueId,
            string Document,
            string SignerId)
        {
            byte[] doc = Convert.FromBase64String(Document);

            // create a new ServerTextControl
            using (TXTextControl.ServerTextControl tx = new TXTextControl.ServerTextControl())
            {
                tx.Create();
                
                // load the signed document
                tx.Load(doc, TXTextControl.BinaryStreamType.InternalUnicodeFormat);

                // save the document as the current signed documet
                tx.Save(Server.MapPath("~/App_Data/documentflows/" + UniqueId + "/signed.tx"),
                    TXTextControl.StreamType.InternalUnicodeFormat);
            }

            // read the document flow structure
            string jsonFlow = System.IO.File.ReadAllText(
                Server.MapPath("~/App_Data/documentflows/" + UniqueId + "/documentflow.json"));

            DocumentFlow documentflow = Newtonsoft.Json.JsonConvert.DeserializeObject<DocumentFlow>(jsonFlow);
            Signer signer = documentflow.Signers.Find(x => x.Id == int.Parse(SignerId));
            signer.SignatureComplete = true; // mark as completed

            // save the flow structure
            System.IO.File.WriteAllText(
                Server.MapPath("~/App_Data/documentflows/" + UniqueId + "/documentflow.json"),
                Newtonsoft.Json.JsonConvert.SerializeObject(documentflow));

            return new JsonResult() { Data = true };
        }
    }
}