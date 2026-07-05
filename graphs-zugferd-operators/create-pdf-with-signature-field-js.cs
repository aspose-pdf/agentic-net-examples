using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "SignedDocument.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (llx, lly, urx, ury)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                // Set a name for the field (used to reference it in JavaScript)
                Name = "Signature1",
                // Optional tooltip shown in PDF viewers
                AlternateName = "Please sign here",
                // Attach a JavaScript action that runs when the field is activated (e.g., clicked)
                // Replace the script with actual validation logic as needed.
                OnActivated = new JavascriptAction("app.alert('Signature field activated.');")
            };

            // Add the signature field to the document's form collection (not to page annotations)
            doc.Form.Add(sigField);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with signature field saved to '{outputPath}'.");
    }
}
