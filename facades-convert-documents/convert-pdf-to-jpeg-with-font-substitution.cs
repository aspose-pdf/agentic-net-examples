using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class PdfToJpegConverter
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where JPEG images will be saved
        const string outputDir = "JpegPages";

        // Font to use when the original PDF references a missing font
        const string substituteFont = "Arial";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Configure font substitution: use the specified default font for any missing fonts
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                DefaultFontName = substituteFont
            };

            // Save the document to a memory stream with the substitution applied
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream, saveOptions);
                pdfStream.Position = 0; // Reset stream position for reading

                // Initialize the PDF converter facade
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the in‑memory PDF (with substituted fonts) to the converter
                    converter.BindPdf(pdfStream);
                    converter.DoConvert();

                    int pageNumber = 1;
                    // Iterate through all pages and save each as a JPEG image
                    while (converter.HasNextImage())
                    {
                        string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.jpg");
                        converter.GetNextImage(outputPath, ImageFormat.Jpeg);
                        pageNumber++;
                    }
                }
            }
        }

        Console.WriteLine("Conversion completed successfully.");
    }
}