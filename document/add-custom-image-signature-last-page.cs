using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "signed_output.pdf";   // result PDF
        const string imagePath = "signature.png";       // custom signature image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            Page lastPage = doc.Pages[doc.Pages.Count];

            // Define the rectangle where the signature field will be placed
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the last page at the defined rectangle
            SignatureField sigField = new SignatureField(lastPage, sigRect);
            // Add the field to the document's form collection
            doc.Form.Add(sigField);

            // OPTIONAL: customize the visual appearance of the signature field
            // Using a custom image as a stamp placed over the field
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                // Position the stamp to match the signature field rectangle
                XIndent = sigRect.LLX,
                YIndent = sigRect.LLY,
                Width   = sigRect.URX - sigRect.LLX,
                Height  = sigRect.URY - sigRect.LLY,
                // Ensure the image is drawn on top of the field content
                Background = false,
                Opacity    = 1.0f
            };

            // Add the image stamp to the same page
            lastPage.AddStamp(imgStamp);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signature with custom image added. Saved to '{outputPdf}'.");
    }
}