using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_a3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(doc);
            editor.PageSize = PageSize.A3; // Set target page size to A3
            editor.ApplyChanges();        // Apply size change to all pages
            editor.Save(outputPath);      // Save the modified PDF
        }

        Console.WriteLine($"PDF saved with A3 page size to '{outputPath}'.");
    }
}