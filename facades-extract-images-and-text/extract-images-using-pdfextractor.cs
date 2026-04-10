using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputFolder = "ExtractedImages";
        Directory.CreateDirectory(outputFolder);

        // Create a simple PDF in memory so we don't depend on an external file.
        using (var pdfStream = new MemoryStream())
        {
            // Build a minimal PDF document.
            using (var doc = new Document())
            {
                doc.Pages.Add(); // add a blank page
                doc.Save(pdfStream);
            }
            pdfStream.Position = 0; // reset stream for reading

            // PdfExtractor implements IDisposable, so a using block will auto‑dispose it.
            using (var extractor = new PdfExtractor())
            {
                // Bind the PDF from the in‑memory stream (no file I/O required).
                extractor.BindPdf(pdfStream);
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    string imagePath = Path.Combine(outputFolder, $"image-{imageIndex}.jpg");
                    extractor.GetNextImage(imagePath);
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
