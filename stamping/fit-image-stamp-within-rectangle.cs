using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "logo.png";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle: create -> load -> save)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define the rectangle on the page where the image should be placed.
            // Fully qualified type avoids ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle targetRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Add the image to the first page.
            // The overload with 'autoAdjustRectangle = true' centers the image
            // inside the rectangle while preserving its aspect ratio.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                pdfDoc.Pages[1].AddImage(imgStream, targetRect, null, true);
            }

            // Save the modified PDF (lifecycle: save)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp fitted and saved to '{outputPdfPath}'.");
    }
}