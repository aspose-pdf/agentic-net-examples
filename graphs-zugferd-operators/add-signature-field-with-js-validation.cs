using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "SignedDocument.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (left, bottom, right, top)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field on the page
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                // Optional: set a name and tooltip (alternate name)
                Name = "Signature1",
                AlternateName = "Please sign here"
            };

            // Add the signature field to the document's form (AcroForm) collection
            // NOTE: Form fields must be added via the Document.Form collection, not directly to page.Annotations
            doc.Form.Add(sigField);

            // Create a JavaScript action that validates the signature when the field is activated
            // This script runs when the user clicks the signature field
            JavascriptAction jsAction = new JavascriptAction(
                "if (event.target.signatureValidate()) {" +
                "    app.alert('Signature is valid.');" +
                "} else {" +
                "    app.alert('Signature validation failed.');" +
                "}"
            );

            // Attach the JavaScript action to the signature field
            sigField.ExecuteFieldJavaScript(jsAction);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with signature field saved to '{outputPath}'.");
    }
}
