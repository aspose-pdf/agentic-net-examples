using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

namespace AsposePdfSignatureJsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple PDF document (self‑contained example)
            using (Document createDoc = new Document())
            {
                // Add a blank page
                createDoc.Pages.Add();
                // Save the temporary PDF that will be reopened
                createDoc.Save("input.pdf");
            }

            // Step 2: Open the PDF and add a signature field with JavaScript validation
            using (Document doc = new Document("input.pdf"))
            {
                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the rectangle for the signature field (llx, lly, urx, ury)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

                // Create the signature field on the page
                SignatureField signatureField = new SignatureField(page, rect);
                signatureField.PartialName = "Signature1";

                // Attach a JavaScript action that runs on validation (when the user signs)
                JavascriptAction jsValidate = new JavascriptAction("app.alert('Signature validated');");
                signatureField.Actions.OnValidate = jsValidate;

                // Add the field to the document's form collection
                doc.Form.Add(signatureField);

                // Save the resulting PDF
                doc.Save("output.pdf");
            }
        }
    }
}
