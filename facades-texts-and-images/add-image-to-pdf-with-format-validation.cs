using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and image to be added
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "picture.png";

        // Verify that the source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Validate image format – only JPG, PNG, GIF, BMP, or TIFF are allowed
        string ext = Path.GetExtension(imagePath).ToLowerInvariant();
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };
        if (Array.IndexOf(allowedExtensions, ext) < 0)
        {
            Console.Error.WriteLine($"Unsupported image format: {ext}");
            return;
        }

        // Use PdfFileMend (Aspose.Pdf.Facades) to add the image to the PDF
        PdfFileMend pdfMend = new PdfFileMend();
        pdfMend.BindPdf(inputPdfPath);                     // Load the PDF document
        // Add the image to page 1; coordinates are lower‑left (10,10) and upper‑right (200,200)
        bool success = pdfMend.AddImage(imagePath, 1, 10, 10, 200, 200);
        if (!success)
        {
            Console.Error.WriteLine("Failed to add the image to the PDF.");
            pdfMend.Close();
            return;
        }

        // Save the modified PDF
        pdfMend.Save(outputPdfPath);
        pdfMend.Close();

        Console.WriteLine($"Image successfully added. Output saved to '{outputPdfPath}'.");
    }
}