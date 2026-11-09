using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // ImageStamp lives in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        // Replace this string with the actual Base64‑encoded image data
        const string base64Image = "iVBORw0KGgoAAAANSUhEUgAA...";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Convert Base64 string to a memory stream
        byte[] imageBytes = Convert.FromBase64String(base64Image);
        using (MemoryStream imgStream = new MemoryStream(imageBytes))
        {
            // Create an ImageStamp from the stream
            ImageStamp imgStamp = new ImageStamp(imgStream)
            {
                Background = false,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            // Convert 50 mm to points (1 point = 1/72 inch)
            double mmToPoints = 72.0 / 25.4;
            imgStamp.XIndent = 0;                     // left offset in points
            imgStamp.YIndent = 50 * mmToPoints;       // 50 mm from the top edge

            // No need to set Width/Height – the stamp uses the image's native size.

            // Load the PDF document
            Document pdfDoc = new Document(inputPath);

            // Ensure the document has at least three pages
            if (pdfDoc.Pages.Count >= 3)
            {
                // Add the stamp to page 3 (pages are 1‑based)
                pdfDoc.Pages[3].AddStamp(imgStamp);
            }
            else
            {
                Console.Error.WriteLine("The PDF does not contain a third page.");
                return;
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added to page 3 and saved as '{outputPath}'.");
    }
}
