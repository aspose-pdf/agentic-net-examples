using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfToJpegWithFontSubstitution
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "Images";
        const string substituteFont = "Arial";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Apply font substitution for missing fonts
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                DefaultFontName = substituteFont
            };

            // Save the document with substituted fonts into a memory stream
            using (MemoryStream pdfStream = new MemoryStream())
            {
                srcDoc.Save(pdfStream, saveOptions);
                pdfStream.Position = 0; // Reset stream position for reading

                // Convert each page to a JPEG image
                using (PdfConverter converter = new PdfConverter())
                {
                    converter.BindPdf(pdfStream);
                    converter.DoConvert();

                    int pageIndex = 1;
                    while (converter.HasNextImage())
                    {
                        string outputPath = Path.Combine(outputFolder, $"page_{pageIndex}.jpg");
                        // Save the current page as JPEG
                        converter.GetNextImage(outputPath, ImageFormat.Jpeg);
                        pageIndex++;
                    }
                }
            }
        }

        Console.WriteLine("Conversion completed.");
    }
}