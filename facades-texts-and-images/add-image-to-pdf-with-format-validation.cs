using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "image.jpg";
        const string outputPdfPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Validate image format (only JPG, PNG, GIF, BMP, TIFF are allowed)
        string ext = Path.GetExtension(imagePath).ToLowerInvariant();
        string[] allowed = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };
        bool isValid = false;
        foreach (string a in allowed)
        {
            if (ext == a) { isValid = true; break; }
        }

        if (!isValid)
        {
            Console.Error.WriteLine($"Unsupported image format: {ext}");
            return;
        }

        // Load PDF and add the image as a stamp
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create an ImageStamp for the validated image file
            ImageStamp imgStamp = new ImageStamp(imagePath)
            {
                // Example positioning – adjust as needed
                Background          = false,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Optional: set opacity (0.0 – 1.0)
                Opacity             = 0.8
            };

            // Add the stamp to the first page (or iterate pages as required)
            Page firstPage = pdfDoc.Pages[1];
            firstPage.AddStamp(imgStamp);

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image added and PDF saved to '{outputPdfPath}'.");
    }
}