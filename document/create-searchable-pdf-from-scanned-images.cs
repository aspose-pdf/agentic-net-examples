// Program.cs
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "scanned.pdf";
        const string outputPath = "searchable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // OCR callback that matches Aspose.Pdf.Document.CallBackGetHocrWithPage signature.
            // Replace the body with a real OCR service that returns HOCR markup.
            Aspose.Pdf.Document.CallBackGetHocrWithPage ocrCallback = (imagePath, pageNumber) =>
            {
                // TODO: integrate actual OCR and return HOCR markup for the image at 'imagePath'.
                return string.Empty;
            };

            // Perform OCR conversion; this overlays invisible text on the image pages.
            // 'flattenImages' set to false to keep original images unchanged.
            doc.Convert(ocrCallback, flattenImages: false);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Searchable PDF saved to '{outputPath}'.");
    }
}