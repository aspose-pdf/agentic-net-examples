using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Define the rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        // The stamp will be scaled to fit inside this rectangle while preserving its aspect ratio.
        Aspose.Pdf.Rectangle targetRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Set the stamp size to match the rectangle dimensions
            imgStamp.Width = targetRect.URX - targetRect.LLX;
            imgStamp.Height = targetRect.URY - targetRect.LLY;

            // Position the stamp at the rectangle's lower‑left corner
            imgStamp.XIndent = targetRect.LLX;
            imgStamp.YIndent = targetRect.LLY;

            // Ensure the stamp is drawn on top of page content
            imgStamp.Background = false;

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(imgStamp);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp fitted and saved to '{outputPdf}'.");
    }
}