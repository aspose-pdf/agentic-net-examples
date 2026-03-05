using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.ods";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: wrap in using)
            using (Document pdfDoc = new Document(inputPath))
            {
                // Create ExcelSaveOptions (required for non‑PDF output)
                ExcelSaveOptions saveOptions = new ExcelSaveOptions();

                // Specify ODS as the target format
                saveOptions.Format = ExcelSaveOptions.ExcelFormat.ODS;

                // Save the document as ODS using the options (save-to-non‑pdf rule)
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to ODS: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}