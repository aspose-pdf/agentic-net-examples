using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "signed_output.pdf"; // result PDF
        const string imagePath = "signature.png";     // custom appearance image

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {imagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            int lastPageIndex = doc.Pages.Count; // last page number

            // Define the rectangle where the signature will appear (coordinates in points)
            double llx = 100; // lower‑left X
            double lly = 100; // lower‑left Y
            double urx = 300; // upper‑right X
            double ury = 200; // upper‑right Y

            // Create the rectangle for the signature field
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a signature field on the last page (field itself is invisible – appearance will be added via an ImageStamp)
            SignatureField sigField = new SignatureField(doc.Pages[lastPageIndex], rect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField, doc.Pages[lastPageIndex].Number);

            // Add a visual appearance (image) on top of the field using ImageStamp – this is the supported way without Facades
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                // Size of the stamp matches the rectangle size
                Width = urx - llx,
                Height = ury - lly,
                // Position – use XIndent/YIndent (ImageStamp has no Left/Top properties)
                XIndent = llx,
                YIndent = lly
            };
            doc.Pages[lastPageIndex].AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signature appearance added and saved to '{outputPath}'.");
    }
}
