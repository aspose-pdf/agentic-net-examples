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

        // Collect extracted images in memory
        List<MemoryStream> extractedImages = new List<MemoryStream>();

        // Extract images from the source PDF
        using (Aspose.Pdf.Facades.PdfExtractor extractor = new Aspose.Pdf.Facades.PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            while (extractor.HasNextImage())
            {
                MemoryStream imgStream = new MemoryStream();
                extractor.GetNextImage(imgStream);
                imgStream.Position = 0;
                extractedImages.Add(imgStream);
            }
        }

        // Create a new PDF/A‑2b document and embed the images as XObjects
        using (Aspose.Pdf.Document pdfaDoc = new Aspose.Pdf.Document())
        {
            foreach (MemoryStream imgStream in extractedImages)
            {
                // Add a new page (A4 size)
                Aspose.Pdf.Page page = pdfaDoc.Pages.Add();
                page.PageInfo.Width = 595;   // points (A4 width)
                page.PageInfo.Height = 842;  // points (A4 height)

                // Embed the image and draw it to fill the page
                page.AddImage(
                    imgStream,
                    new Aspose.Pdf.Rectangle(0, 0, page.PageInfo.Width, page.PageInfo.Height));

                // The stream is no longer needed after embedding
                imgStream.Dispose();
            }

            // Convert the document to PDF/A‑2b compliance
            pdfaDoc.Convert(
                "conversion_log.xml",
                Aspose.Pdf.PdfFormat.PDF_A_2B,
                Aspose.Pdf.ConvertErrorAction.Delete);

            // Save the PDF/A‑2b compliant document
            pdfaDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF/A‑2b document created: {outputPdfPath}");
    }
}