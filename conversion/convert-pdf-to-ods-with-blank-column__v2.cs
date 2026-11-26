using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "financials.pdf";
        const string outputPath = "financials_blank.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure ExcelSaveOptions for ODS output
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Set the target format to ODS
                    Format = ExcelSaveOptions.ExcelFormat.ODS,
                    // Insert a blank column at the beginning of each sheet
                    InsertBlankColumnAtFirst = true
                };

                // Save the PDF as ODS using the configured options
                pdfDoc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion completed: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}