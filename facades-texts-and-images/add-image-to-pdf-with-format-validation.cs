using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string imagePath  = "picture.png";        // image to add
        const string outputPdf  = "output.pdf";         // result PDF

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Validate image format (allow only JPG, PNG, GIF, BMP, TIFF)
        string ext = Path.GetExtension(imagePath).ToLowerInvariant();
        string[] allowed = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };
        if (Array.IndexOf(allowed, ext) < 0)
        {
            Console.Error.WriteLine($"Unsupported image format: {ext}");
            return;
        }

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Initialize PdfFileMend with the loaded document
            using (PdfFileMend mend = new PdfFileMend(pdfDoc))
            {
                // Add the image to page 1 at the desired rectangle (lower‑left X/Y, upper‑right X/Y)
                // Example coordinates place the image at (10,10) – (100,100)
                bool added = mend.AddImage(imagePath, 1, 10f, 10f, 100f, 100f);
                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the image to the PDF.");
                    return;
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Image added successfully. Output saved to '{outputPdf}'.");
    }
}