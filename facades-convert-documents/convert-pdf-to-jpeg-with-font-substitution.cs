using System;
using System.IO;
using System.Drawing.Imaging;          // ImageFormat enum
using Aspose.Pdf;                     // Document, PdfSaveOptions
using Aspose.Pdf.Facades;             // PdfConverter

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputDir      = "Images";            // folder for JPEGs
        const string fallbackFont   = "Arial";             // font used when original is missing

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Apply custom font substitution by saving to a memory stream
            // with a default font name for any missing fonts.
            using (MemoryStream tempPdf = new MemoryStream())
            {
                PdfSaveOptions saveOptions = new PdfSaveOptions();
                saveOptions.DefaultFontName = fallbackFont;   // rule: set default font for missing fonts
                doc.Save(tempPdf, saveOptions);
                tempPdf.Position = 0; // reset stream for reading

                // Convert each page of the (font‑substituted) PDF to JPEG images
                PdfConverter converter = new PdfConverter();
                converter.BindPdf(tempPdf);
                converter.DoConvert();

                int pageIndex = 1;
                while (converter.HasNextImage())
                {
                    string outFile = Path.Combine(outputDir, $"page_{pageIndex}.jpg");
                    // GetNextImage with JPEG format (default quality)
                    converter.GetNextImage(outFile, ImageFormat.Jpeg);
                    pageIndex++;
                }

                // Release resources held by the converter
                converter.Close();
            }
        }

        Console.WriteLine("PDF has been converted to JPEG images.");
    }
}