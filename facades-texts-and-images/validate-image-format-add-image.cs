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

        string extension = Path.GetExtension(imagePath).ToLowerInvariant();
        bool isSupported = extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp" || extension == ".tif" || extension == ".tiff";

        if (!isSupported)
        {
            Console.Error.WriteLine($"Unsupported image format: {extension}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            page.AddImage(imagePath, rect);
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image added and saved to '{outputPdf}'.");
    }
}