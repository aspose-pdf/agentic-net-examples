using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp lives in this namespace

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the image to stamp, and the output PDF
        const string pdfPath   = "source.pdf";
        const string imagePath = "stamp.png";
        const string outPath   = "stamped.pdf";

        // Validate input files
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the image into a memory stream
        using (MemoryStream imgStream = new MemoryStream(File.ReadAllBytes(imagePath)))
        {
            // Create an ImageStamp from the memory stream
            ImageStamp stamp = new ImageStamp(imgStream)
            {
                // Scale the stamp to 50% of its original size
                Zoom = 0.5, // double‑precision scaling factor (50 %)

                // Optional positioning – keep default bottom‑left corner
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Bottom,
                XIndent = 0f,
                YIndent = 0f,

                // Place the stamp in the foreground (over existing content)
                Background = false
            };

            // Load the PDF document (Document implements IDisposable)
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Add the stamp to the first page (pages are 1‑based)
                pdfDoc.Pages[1].AddStamp(stamp);

                // Save the modified PDF
                pdfDoc.Save(outPath);
            }
        }

        Console.WriteLine($"Image stamp applied and saved to '{outPath}'.");
    }
}
