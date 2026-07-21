using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string imagePath = "image.png";
        const string outputPdf = "output.pdf";

        // Verify source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Validate image format (allow only JPG, PNG, GIF, BMP, TIFF)
        string ext = Path.GetExtension(imagePath).ToLowerInvariant();
        string[] allowed = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };
        bool isValid = Array.Exists(allowed, e => e == ext);
        if (!isValid)
        {
            Console.Error.WriteLine($"Unsupported image format: {ext}");
            return;
        }

        // Use PdfFileMend facade to add the image
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the existing PDF document
            mend.BindPdf(inputPdf);

            // Open the image stream and add it to page 1
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Coordinates: lower-left (50,700), upper-right (200,850)
                bool added = mend.AddImage(imgStream, 1, 50f, 700f, 200f, 850f);
                if (!added)
                {
                    Console.Error.WriteLine("Failed to add image to the PDF.");
                }
            }

            // Save the modified PDF
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Image added and saved to '{outputPdf}'.");
    }
}