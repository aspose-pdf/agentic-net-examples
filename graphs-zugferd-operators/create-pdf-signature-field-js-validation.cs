using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "signed_with_js.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field on the page
            SignatureField sigField = new SignatureField(page, rect)
            {
                PartialName = "Signature1",
                AlternateName = "Please sign here"
            };

            // Add the signature field to the document's form collection (not the page)
            doc.Form.Add(sigField);

            // JavaScript that validates the signature when the field is signed
            JavascriptAction js = new JavascriptAction(
                "if (event.target.signatureValidate()) { app.alert('Signature is valid'); } else { app.alert('Signature is invalid'); }");

            // Attach the JavaScript action to the signature field – use the OnValidate action
            sigField.Actions.OnValidate = js;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}
