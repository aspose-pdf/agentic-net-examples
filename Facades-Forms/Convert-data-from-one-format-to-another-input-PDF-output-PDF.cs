using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle managed by using)
        using (Document doc = new Document(inputPath))
        {
            // Demonstrate usage of a Facade class from Aspose.Pdf.Facades
            // Here we bind the same document to PdfConverter (no image conversion needed)
            PdfConverter converter = new PdfConverter(doc);
            // Optional: converter.DoConvert(); // prepares conversion if needed

            // Save the document as PDF (output format matches input)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully converted and saved to '{outputPath}'.");
    }
}