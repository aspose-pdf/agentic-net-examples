using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string imagePath = "logo.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load PDF to obtain page dimensions
        using (Document pdfDoc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = pdfDoc.Pages[1];
            double pageWidth = page.PageInfo.Width;   // Width is double
            double pageHeight = page.PageInfo.Height; // Height is double

            // Desired image size (points)
            double imageWidth = 100.0;
            double imageHeight = 100.0;
            // Margin from page edges
            double margin = 20.0;

            // Calculate coordinates for bottom‑right placement
            double lowerLeftX = pageWidth - imageWidth - margin;
            double lowerLeftY = margin;
            double upperRightX = pageWidth - margin;
            double upperRightY = margin + imageHeight;

            // Use PdfFileMend facade correctly (bind PDF first)
            var mend = new Aspose.Pdf.Facades.PdfFileMend();
            mend.BindPdf(inputPath);

            bool added = mend.AddImage(
                imagePath,
                1,
                (float)lowerLeftX,
                (float)lowerLeftY,
                (float)upperRightX,
                (float)upperRightY);

            mend.Save(outputPath);
            mend.Close();

            if (added)
                Console.WriteLine($"Image added successfully. Saved as {outputPath}");
            else
                Console.Error.WriteLine("Failed to add image.");
        }
    }
}
