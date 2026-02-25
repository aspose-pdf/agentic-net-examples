using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.docx";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure DOCX save options
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Use full flow recognition for maximum editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Specify the output format explicitly
                    Format = DocSaveOptions.DocFormat.DocX
                };

                // Save as DOCX using the options (required to get non‑PDF output)
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to DOCX: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}