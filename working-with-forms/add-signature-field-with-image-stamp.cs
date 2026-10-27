using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string stampImage = "signature_stamp.png"; // predefined image

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the signature field (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "ClientSignature"
            };

            // Add the signature field to the document's form
            doc.Form.Add(sigField);

            // Add a visual representation of the signature using an ImageStamp.
            // The stamp is placed exactly over the signature field rectangle.
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                BottomMargin = rect.LLY,
                LeftMargin   = rect.LLX,
                Width        = rect.URX - rect.LLX,
                Height       = rect.URY - rect.LLY,
                // Optional: keep the image's original aspect ratio
                // IsBackground = false
            };
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature field added and saved to '{outputPath}'.");
    }
}
