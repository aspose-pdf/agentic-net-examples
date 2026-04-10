using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "SignedDocument.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field on the page
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                Name = "Signature1",               // Field name
                AlternateName = "Sign Here",       // Tooltip shown in PDF viewers
                Required = true                    // Mark as required
            };

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Attach JavaScript that validates the signature when the field is activated.
            // Use the valid OnValidate action (AnnotationActionCollection does not expose OnMouseUp).
            sigField.Actions.OnValidate = new JavascriptAction(
                "if (event.target.signatureValidate()) {" +
                "    app.alert('Signature is valid');" +
                "} else {" +
                "    app.alert('Signature is invalid');" +
                "}"
            );

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with signature field saved to '{outputPath}'.");
    }
}
