using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string imagePath = "image.png";
        const string outputPath = "output.pdf";

        // Verify source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Validate image format (allow only JPG, PNG, GIF, BMP, TIFF)
        string ext = Path.GetExtension(imagePath).ToLowerInvariant();
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };
        if (Array.IndexOf(allowedExtensions, ext) < 0)
        {
            Console.Error.WriteLine($"Unsupported image format: {ext}");
            return;
        }

        // Add the image to the PDF using PdfFileMend facade
        using (PdfFileMend pdfMend = new PdfFileMend())
        {
            // Bind the existing PDF document
            pdfMend.BindPdf(pdfPath);

            // Add the image to page 1 with specified rectangle coordinates
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                bool added = pdfMend.AddImage(imgStream, 1, 10f, 10f, 200f, 200f);
                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the image to the PDF.");
                    return;
                }
            }

            // Save the modified PDF
            pdfMend.Save(outputPath);
        }

        Console.WriteLine($"Image successfully added. Output saved to '{outputPath}'.");
    }
}