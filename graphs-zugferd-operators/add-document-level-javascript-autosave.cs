using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

namespace AddDocumentLevelJavascriptAutosave
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: create a sample PDF (self‑contained example)
            using (Document sourceDoc = new Document())
            {
                sourceDoc.Pages.Add();
                sourceDoc.Save("input.pdf");
            }

            // Step 2: load the PDF and add document‑level JavaScript
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // JavaScript that saves the document every 10 seconds
                string jsCode = "app.setInterval('this.saveAs({cPath:\"autosave.pdf\"});', 10000);";

                // Document.JavaScript is read‑only; use OpenAction with JavascriptAction instead
                pdfDoc.OpenAction = new JavascriptAction(jsCode);

                pdfDoc.Save("output.pdf");
            }
        }
    }
}
