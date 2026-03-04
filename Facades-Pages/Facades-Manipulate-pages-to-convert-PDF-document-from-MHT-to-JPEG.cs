using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for JPEG output

class Program
{
    static void Main()
    {
        // Input MHT file and output directory for JPEG images
        const string mhtPath = "input.mht";
        const string outputDir = "JpegPages";

        if (!File.Exists(mhtPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the MHT file into a PDF Document using MhtLoadOptions
        using (Document pdfDoc = new Document(mhtPath, new MhtLoadOptions()))
        {
            // Initialize the PdfConverter facade
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the loaded PDF document to the converter
                converter.BindPdf(pdfDoc);
                // Prepare the converter (required before extracting images)
                converter.DoConvert();

                int pageIndex = 1;
                // Iterate over all pages and save each as a JPEG image
                while (converter.HasNextImage())
                {
                    string outputFile = Path.Combine(outputDir, $"Page_{pageIndex}.jpg");
                    // Save the current page as JPEG with default quality
                    converter.GetNextImage(outputFile, ImageFormat.Jpeg);
                    pageIndex++;
                }
            }

            // Optionally, save the intermediate PDF if needed
            // pdfDoc.Save("intermediate.pdf");
        }

        Console.WriteLine("MHT to JPEG conversion completed.");
    }
}