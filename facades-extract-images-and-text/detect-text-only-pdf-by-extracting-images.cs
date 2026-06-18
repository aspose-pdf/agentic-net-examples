using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF to be examined
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        bool hasImages = false;

        // PdfExtractor implements IDisposable, so wrap it in a using block
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Start the image extraction process
            extractor.ExtractImage();

            // Iterate through all extracted images.
            // GetNextImage writes the image to a stream; we use a MemoryStream
            // to avoid creating any files on disk.
            while (extractor.HasNextImage())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    extractor.GetNextImage(ms);
                    if (ms.Length > 0)
                    {
                        hasImages = true;
                        // An image was found; we can stop further extraction.
                        break;
                    }
                }
            }
        }

        // Output the result
        if (hasImages)
            Console.WriteLine("The PDF contains images.");
        else
            Console.WriteLine("The PDF is text‑only.");
    }
}