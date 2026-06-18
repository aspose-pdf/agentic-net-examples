using System;
using System.IO;
using System.Drawing.Imaging; // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_pdfa2b.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Extract images from the source PDF using PdfExtractor (Facades API)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            // Create a new PDF document that will hold the extracted images
            using (Document newDoc = new Document())
            {
                // Loop through all extracted images
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream (PNG format)
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imgStream, ImageFormat.Png);
                        imgStream.Position = 0; // reset for reading

                        // Add a new page for each image
                        Page page = newDoc.Pages.Add();

                        // Add the image as an XObject resource
                        page.Resources.Images.Add(imgStream);

                        // Place the image on the page covering the whole page area
                        // Use fully qualified Aspose.Pdf.Rectangle to avoid ambiguity
                        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                            0,                                   // lower‑left X
                            0,                                   // lower‑left Y
                            page.PageInfo.Width,                 // upper‑right X
                            page.PageInfo.Height);               // upper‑right Y

                        // The same stream is used for AddImage; the image is referenced as an XObject
                        page.AddImage(imgStream, rect);
                    }
                }

                // Convert the assembled document to PDF/A‑2b compliance
                // Log conversion details to a temporary file (can be ignored after)
                newDoc.Convert("conversion_log.xml", PdfFormat.PDF_A_2B, ConvertErrorAction.Delete);

                // Save the PDF/A‑2b compliant document
                newDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF/A‑2b document created: {outputPdfPath}");
    }
}