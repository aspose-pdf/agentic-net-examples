using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_pdfa2b.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Extract all images from the source PDF using PdfExtractor
        // -----------------------------------------------------------------
        List<MemoryStream> extractedImages = new List<MemoryStream>();

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                MemoryStream imgStream = new MemoryStream();
                extractor.GetNextImage(imgStream);
                imgStream.Position = 0;               // reset for later reading
                extractedImages.Add(imgStream);
            }
        }

        // -----------------------------------------------------------------
        // Step 2: Create a new PDF document and embed each image as an XObject
        // -----------------------------------------------------------------
        using (Document pdfaDoc = new Document())
        {
            foreach (MemoryStream imgStream in extractedImages)
            {
                // Add a new page for each image
                Page page = pdfaDoc.Pages.Add();

                // Define a rectangle that covers the whole page
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    0,
                    0,
                    page.MediaBox.Width,
                    page.MediaBox.Height);

                // Embed the image on the page (the image is stored as an XObject internally)
                page.AddImage(imgStream, rect);

                // Reset the stream position in case it is reused later
                imgStream.Position = 0;
            }

            // -----------------------------------------------------------------
            // Step 3: Convert the document to PDF/A‑2b compliance
            // -----------------------------------------------------------------
            string logFile = Path.Combine(Path.GetDirectoryName(outputPdfPath) ?? ".", "pdfa2b_conversion.log");
            pdfaDoc.Convert(logFile, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

            // Save the PDF/A‑2b document
            pdfaDoc.Save(outputPdfPath);
        }

        // Cleanup extracted image streams
        foreach (MemoryStream ms in extractedImages)
        {
            ms.Dispose();
        }

        Console.WriteLine($"PDF/A‑2b document created at '{outputPdfPath}'.");
    }
}