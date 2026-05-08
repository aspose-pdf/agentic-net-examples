using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_pdfa2b.pdf";
        const string conversionLog = "conversion_log.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Extract images from the source PDF using the Facade API
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            // Create a new PDF document that will become PDF/A‑2b
            using (Document pdfaDoc = new Document())
            {
                // Iterate over all extracted images
                while (extractor.HasNextImage())
                {
                    // Store the current image in a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0;

                        // Add a new page for each image
                        Page page = pdfaDoc.Pages.Add();

                        // Use the page size to fill the whole page with the image
                        double pageWidth = page.PageInfo.Width;
                        double pageHeight = page.PageInfo.Height;

                        // Embed the image as an XObject
                        page.AddImage(imgStream, new Aspose.Pdf.Rectangle(0, 0, pageWidth, pageHeight));
                    }
                }

                // Convert the document to PDF/A‑2b compliance
                pdfaDoc.Convert(conversionLog, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

                // Save the compliant document
                pdfaDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF/A‑2b document created: {outputPdf}");
    }
}
