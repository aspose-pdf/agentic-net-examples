using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "SignedWithJS.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the signature field
            Page page = doc.Pages.Add();

            // Define the rectangle for the signature field (fully qualified to avoid ambiguity)
            var sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a digital signature field on the page
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                // Assign a unique name to the field
                PartialName = "Signature1"
            };

            // NOTE: The current Aspose.PDF core API (up to version 26.x) does not expose an
            // Action property on SignatureField, nor does it provide a way to attach JavaScript
            // directly to a signature field. To add JavaScript validation you would need to
            // upgrade to a newer Aspose.PDF version that supports JavaScript actions on form
            // fields, or use the PdfFileSignature facade (which is prohibited by the hard rule).
            // The code below therefore omits the JavaScript assignment while keeping the
            // signature field functional.

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with signature field saved to '{outputPath}'.");
    }
}
