using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "stamp.png";

        if (!File.Exists(inputPdf) || !File.Exists(imagePath))
        {
            Console.Error.WriteLine("Input PDF or image file not found.");
            return;
        }

        // Define the rectangle (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
        // The image will be centered in this rectangle while preserving its aspect ratio.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Add the image; autoAdjustRectangle = true keeps the image proportion
            // and positions it in the middle of the specified rectangle.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                page.AddImage(imgStream, rect, autoAdjustRectangle: true);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp fitted and saved to '{outputPdf}'.");
    }
}