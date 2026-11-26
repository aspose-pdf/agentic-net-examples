using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "agenda.pdf";
        const string outputPath = "agenda.pptx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Default PPTX save options are required for non‑PDF output.
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                doc.Save(outputPath, pptxOptions);
            }

            Console.WriteLine($"PDF successfully converted to PPTX: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}