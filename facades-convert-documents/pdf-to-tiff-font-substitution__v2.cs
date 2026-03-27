using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.tiff";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Substitute Courier with Liberation Mono for rendering
        SimpleFontSubstitution substitution = new SimpleFontSubstitution("Courier", "Liberation Mono");
        FontRepository.Substitutions.Add(substitution);

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PDF converter
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(doc);
            converter.StartPage = 1;
            converter.EndPage = doc.Pages.Count;
            converter.Resolution = new Resolution(300);

            // Perform the conversion
            converter.DoConvert();

            // Save all pages as a single multi‑page TIFF file
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"PDF converted to TIFF: {outputPath}");
    }
}
