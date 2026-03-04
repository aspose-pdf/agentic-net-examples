using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Optional: configure PDF save options (e.g., default font fallback)
                PdfSaveOptions saveOptions = new PdfSaveOptions();
                // saveOptions.DefaultFontName = "Arial";

                // Save the document as PDF to the specified file system path
                doc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF processing: {ex.Message}");
        }
    }
}