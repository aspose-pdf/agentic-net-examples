using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF containing images
        const string outputPdf  = "output_pdfa2b.pdf";  // PDF/A‑2b result
        const string logPath    = "conversion.log";    // conversion log (optional)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract images from the source PDF using PdfExtractor (Facade)
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();                     // extract all images

            // -----------------------------------------------------------------
            // 2. Create a new PDF document and embed each extracted image as an XObject
            // -----------------------------------------------------------------
            using (Document pdfaDoc = new Document())
            {
                int imageIndex = 1;

                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0; // reset for reading

                        // Add a new page for each image (optional: you can place multiple per page)
                        Page page = pdfaDoc.Pages.Add();

                        // Define the rectangle where the image will be placed.
                        // Here we use the image's original dimensions (if known) or fit to page.
                        // For simplicity, fit the image to the page margins.
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                            page.PageInfo.Width * 0.05,   // llx (5% from left)
                            page.PageInfo.Height * 0.05,  // lly (5% from bottom)
                            page.PageInfo.Width * 0.95,   // urx (95% width)
                            page.PageInfo.Height * 0.95   // ury (95% height)
                        );

                        // Add the image to the page. This creates an XObject internally.
                        page.AddImage(imgStream, rect);
                    }

                    imageIndex++;
                }

                // -------------------------------------------------------------
                // 3. Convert the document to PDF/A‑2b compliance
                // -------------------------------------------------------------
                // The Convert method writes a log file; you can ignore it if not needed.
                pdfaDoc.Convert(logPath, PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

                // -------------------------------------------------------------
                // 4. Save the PDF/A‑2b document
                // -------------------------------------------------------------
                pdfaDoc.Save(outputPdf);
            }

            // PdfExtractor implements IDisposable; the using block ensures proper cleanup.
        }

        Console.WriteLine($"PDF/A‑2b document created: {outputPdf}");
    }
}