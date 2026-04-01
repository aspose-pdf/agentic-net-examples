using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imagePath = "image.png";
        const string outputPdf = "output.pdf";

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

        // Validate allowed image formats: JPG, PNG, GIF, BMP, TIFF
        string extension = Path.GetExtension(imagePath).ToLowerInvariant();
        if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif" && extension != ".bmp" && extension != ".tif" && extension != ".tiff")
        {
            Console.Error.WriteLine("Unsupported image format. Allowed formats: JPG, PNG, GIF, BMP, TIFF.");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the image will be placed (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Add the image to the first page
                doc.Pages[1].AddImage(imgStream, rect);
            }

            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image added and saved to '{outputPdf}'.");
    }
}
