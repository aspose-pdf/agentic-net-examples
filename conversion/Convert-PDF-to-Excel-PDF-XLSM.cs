using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.xlsm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Initialize Excel save options
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();

            // Specify the macro‑enabled Excel format (XLSM)
            saveOptions.Format = ExcelSaveOptions.ExcelFormat.XLSM;

            // Optional: control worksheet creation, e.g. one worksheet per PDF page
            // saveOptions.MinimizeTheNumberOfWorksheets = true;

            // Save the document as XLSM using the options
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"PDF successfully converted to XLSM: '{outputPath}'");
    }
}