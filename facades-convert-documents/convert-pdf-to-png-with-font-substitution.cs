using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;          // for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;                 // for SimpleFontSubstitution and FontRepository

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "PngPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Register a font substitution: replace missing Helvetica with Times New Roman
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Times New Roman"));

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        // PdfConverter also implements IDisposable
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the loaded document to the converter
            converter.BindPdf(doc);

            // (Optional) you can still configure other rendering options here
            // RenderingOptions renderOpts = new RenderingOptions();
            // converter.RenderingOptions = renderOpts;

            // Convert all pages
            converter.StartPage = 1;
            converter.EndPage   = doc.Pages.Count;

            int pageNumber = 1;
            while (converter.HasNextImage())
            {
                string outPath = Path.Combine(outputDir, $"page_{pageNumber}.png");
                // Save the current page as PNG
                converter.GetNextImage(outPath, ImageFormat.Png);
                pageNumber++;
            }
        }

        Console.WriteLine($"PDF pages have been converted to PNG images in '{outputDir}'.");
    }
}
