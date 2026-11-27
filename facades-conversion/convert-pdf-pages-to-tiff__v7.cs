using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // Resolution is here

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "TiffPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the document once to obtain the page count
        Document pdfDoc = new Document(inputPdf);
        int pageCount = pdfDoc.Pages.Count;

        // PdfConverter implements IDisposable – wrap it in a using block
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the PDF document
            converter.BindPdf(inputPdf);

            // Define the overall page range (all pages)
            converter.StartPage = 1;
            converter.EndPage   = pageCount;

            // Set the coordinate type AFTER defining StartPage as required.
            // PdfConverter uses PageCoordinateType, not the non‑existent CoordinateType enum.
            converter.CoordinateType = PageCoordinateType.CropBox;

            // Set desired resolution (DPI) for the output images
            converter.Resolution = new Resolution(150);

            // Convert each page individually and save as a separate TIFF file
            for (int page = converter.StartPage; page <= converter.EndPage; page++)
            {
                // Restrict conversion to the current page only
                converter.StartPage = page;
                converter.EndPage   = page;

                // Perform the conversion for the current page
                converter.DoConvert();

                // Save the converted page as a TIFF image
                string outPath = Path.Combine(outputDir, $"Page_{page}.tiff");
                converter.SaveAsTIFF(outPath);
                Console.WriteLine($"Saved TIFF: {outPath}");
            }

            // Release resources associated with the converter
            converter.Close();
        }
    }
}
