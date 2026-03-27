using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

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

        // Apply custom font substitution: Symbol → Arial Unicode MS
        FontRepository.Substitutions.Add(new SimpleFontSubstitution("Symbol", "Arial Unicode MS"));

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Convert all pages to a single multi‑page TIFF image
            PdfConverter converter = new PdfConverter();
            converter.BindPdf(doc);
            converter.StartPage = 1;
            converter.EndPage = doc.Pages.Count;
            converter.DoConvert();
            converter.SaveAsTIFF(outputPath);
        }

        Console.WriteLine($"TIFF saved to '{outputPath}'.");
    }
}
