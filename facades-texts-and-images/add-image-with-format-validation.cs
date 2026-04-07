using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPdf);
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine("Image file not found: " + imagePath);
            return;
        }

        // Validate image format (JPG, PNG, GIF, BMP, TIFF)
        string extension = Path.GetExtension(imagePath).ToLowerInvariant();
        bool isValid = extension == ".jpg" || extension == ".jpeg" || extension == ".png" ||
                       extension == ".gif" || extension == ".bmp" || extension == ".tif" ||
                       extension == ".tiff";

        if (!isValid)
        {
            Console.Error.WriteLine("Unsupported image format: " + extension);
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                doc.Pages.Add();
            }

            Page page = doc.Pages[1];
            // Define the rectangle where the image will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            // Add the image to the page
            page.AddImage(imagePath, rect);
            doc.Save(outputPdf);
        }

        Console.WriteLine("Image added and PDF saved to " + outputPdf);
    }
}