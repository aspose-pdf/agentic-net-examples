using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Verify image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Validate image format (JPG, PNG, GIF, BMP, TIFF)
        if (!IsSupportedImage(imagePath))
        {
            Console.Error.WriteLine("Unsupported image format. Allowed extensions: .jpg, .jpeg, .png, .gif, .bmp, .tif, .tiff");
            return;
        }

        // Use PdfFileMend (Facade) to add the image
        using (PdfFileMend mender = new PdfFileMend())
        {
            // Bind the existing PDF document
            mender.BindPdf(inputPdf);

            // Add the image to page 1 at the specified rectangle
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // lower-left (10,10), upper-right (200,200) – adjust as needed
                mender.AddImage(imgStream, 1, 10f, 10f, 200f, 200f);
            }

            // Save the modified PDF
            mender.Save(outputPdf);
            mender.Close();
        }

        Console.WriteLine($"Image added successfully. Output saved to '{outputPdf}'.");
    }

    // Helper to check allowed image extensions
    static bool IsSupportedImage(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext == ".jpg" || ext == ".jpeg" ||
               ext == ".png" ||
               ext == ".gif" ||
               ext == ".bmp" ||
               ext == ".tif" || ext == ".tiff";
    }
}