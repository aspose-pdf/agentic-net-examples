using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: PDF file path and signature image path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <app> <pdfPath> <signatureImagePath>");
            return;
        }

        string pdfPath = args[0];
        string imagePath = args[1];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {imagePath}");
            return;
        }

        // Output file will be the original name with "_signed.pdf" suffix
        string outputPath = Path.Combine(
            Path.GetDirectoryName(pdfPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");

        try
        {
            // Load the source PDF using the high‑level Document API (PdfContentEditor does not expose AddImageStamp)
            using (Document doc = new Document(pdfPath))
            {
                // Define the rectangle where the signature will be placed (points, 1/72 inch)
                double llx = 400; // left X
                double lly = 50;  // lower Y
                double urx = 550; // right X
                double ury = 150; // upper Y

                // Create an ImageStamp and position it using margins and size
                ImageStamp imgStamp = new ImageStamp(imagePath)
                {
                    // Width and Height are derived from the rectangle size
                    Width = urx - llx,
                    Height = ury - lly,
                    // Margins position the stamp on the page
                    LeftMargin = llx,
                    BottomMargin = lly,
                    // Optional: keep the original image aspect ratio
                    // ScaleProportionally = true
                };

                // Add the stamp to the first page (pages are 1‑based)
                doc.Pages[1].AddStamp(imgStamp);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Signed PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
