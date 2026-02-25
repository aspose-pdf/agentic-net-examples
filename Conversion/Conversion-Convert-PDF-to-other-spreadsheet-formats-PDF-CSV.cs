using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.csv";

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
                // Configure ExcelSaveOptions to produce CSV output
                ExcelSaveOptions saveOptions = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.CSV
                };

                // Save the document as CSV using the explicit SaveOptions
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}