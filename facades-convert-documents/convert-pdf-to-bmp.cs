using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF document (self‑contained example)
        string samplePdfPath = "sample.pdf";
        using (Document sampleDoc = new Document())
        {
            // Add a page and a simple text fragment
            Page page = sampleDoc.Pages.Add();
            TextFragment fragment = new TextFragment("Sample PDF for BMP conversion");
            page.Paragraphs.Add(fragment);
            // Save the sample PDF to disk
            sampleDoc.Save(samplePdfPath);
        }

        // Load the PDF document to be converted
        using (Document pdfDocument = new Document(samplePdfPath))
        {
            // Initialise PdfConverter with the loaded document
            using (PdfConverter converter = new PdfConverter(pdfDocument))
            {
                // Configure conversion settings
                converter.StartPage = 1;
                converter.EndPage = pdfDocument.Pages.Count;
                converter.Resolution = new Resolution(300); // 300 DPI
                converter.CoordinateType = PageCoordinateType.CropBox; // use CropBox (default)

                // Perform initial conversion preparation
                converter.DoConvert();

                int pageNumber = converter.StartPage;
                while (converter.HasNextImage())
                {
                    string outputBmp = $"image{pageNumber}_out.bmp";
                    using (FileStream bmpStream = new FileStream(outputBmp, FileMode.Create))
                    {
                        // Save each page as BMP image
                        converter.GetNextImage(bmpStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                    pageNumber++;
                }
            }
        }
    }
}