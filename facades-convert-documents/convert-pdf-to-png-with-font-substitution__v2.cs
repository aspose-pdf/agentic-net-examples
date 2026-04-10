using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "png_output";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Load the PDF and apply a default font substitution for any missing fonts
        using (Document pdfDoc = new Document(inputPdf))
        {
            // PdfSaveOptions.DefaultFontName replaces missing fonts with the specified font (e.g., Arial)
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                DefaultFontName = "Arial"
            };

            using (MemoryStream tempStream = new MemoryStream())
            {
                // Save the document to a memory stream with the substitution applied
                pdfDoc.Save(tempStream, saveOptions);
                tempStream.Position = 0;

                // Initialize the converter for PNG output
                PdfConverter converter = new PdfConverter();
                converter.BindPdf(tempStream);

                // Convert all pages
                converter.StartPage = 1;
                converter.EndPage = pdfDoc.Pages.Count;

                // Set resolution (e.g., 300 DPI)
                converter.Resolution = new Resolution(300);

                // Enable font hinting for better text rendering
                Aspose.Pdf.RenderingOptions renderingOpts = new Aspose.Pdf.RenderingOptions
                {
                    UseFontHinting = true
                };
                converter.RenderingOptions = renderingOpts;

                int pageIndex = 1;
                while (converter.HasNextImage())
                {
                    string outPath = Path.Combine(outputFolder, $"page_{pageIndex}.png");
                    // Save each page as PNG
                    converter.GetNextImage(outPath, ImageFormat.Png);
                    pageIndex++;
                }

                // Release converter resources
                converter.Close();
            }
        }

        Console.WriteLine("PDF successfully converted to PNG images.");
    }
}