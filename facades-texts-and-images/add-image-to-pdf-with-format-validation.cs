using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    // Allowed image extensions (case‑insensitive)
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };

    // Returns true if the file has one of the allowed image extensions
    private static bool IsSupportedImage(string imagePath)
    {
        if (string.IsNullOrWhiteSpace(imagePath))
            return false;

        string ext = Path.GetExtension(imagePath);
        if (string.IsNullOrEmpty(ext))
            return false;

        ext = ext.ToLowerInvariant();
        foreach (var allowed in AllowedExtensions)
        {
            if (ext == allowed)
                return true;
        }
        return false;
    }

    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string imageFile = "picture.png";
        const string outputPdf = "output.pdf";

        // Verify source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imageFile))
        {
            Console.Error.WriteLine($"Image file not found: {imageFile}");
            return;
        }

        // Validate image format
        if (!IsSupportedImage(imageFile))
        {
            Console.Error.WriteLine("Unsupported image format. Allowed formats: JPG, PNG, GIF, BMP, TIFF.");
            return;
        }

        // Add the image to page 1 at the specified rectangle
        using (PdfFileMend mender = new PdfFileMend())
        {
            mender.BindPdf(inputPdf);
            // lowerLeftX, lowerLeftY, upperRightX, upperRightY define the image rectangle
            bool added = mender.AddImage(imageFile, 1, 10f, 10f, 200f, 200f);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add the image to the PDF.");
                return;
            }

            mender.Save(outputPdf);
            mender.Close();
        }

        Console.WriteLine($"Image added successfully. Output saved to '{outputPdf}'.");
    }
}