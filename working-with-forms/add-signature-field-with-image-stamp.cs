using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_signed.pdf";  // result PDF
        const string stampImage = "signature_stamp.png"; // predefined image for appearance

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the signature field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                Name = "ClientSignature"   // field name
            };

            // Add the field to the document's form
            doc.Form.Add(sigField);

            // Create an image stamp using the predefined image
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Position the stamp to match the signature field rectangle
                // XIndent/YIndent are the lower‑left corner coordinates
                XIndent = sigRect.LLX,
                YIndent = sigRect.LLY,
                // Set width/height to match the rectangle size
                Width  = sigRect.URX - sigRect.LLX,
                Height = sigRect.URY - sigRect.LLY,
                // Optional visual settings
                Background = false,
                Opacity    = 1.0f
            };

            // Add the image stamp to the same page (page 1)
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and appearance set. Saved to '{outputPath}'.");
    }
}