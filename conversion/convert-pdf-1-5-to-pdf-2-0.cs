using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF 1.5 source
        const string outputPath = "ConvertedToPdf2.pdf"; // PDF 2.0 target

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF 2.0 format (v_2_0).
            // The conversion preserves all existing metadata (Info, custom metadata, etc.).
            doc.Convert("conversion_log.xml", PdfFormat.v_2_0, ConvertErrorAction.Delete);

            // Save the converted document. No SaveOptions needed because the output is PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Conversion completed: '{outputPath}'");
    }
}
