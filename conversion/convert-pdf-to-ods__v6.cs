using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // optional, not needed for conversion but harmless

class Program
{
    static void Main()
    {
        const string inputPath  = "catalog.pdf";
        const string outputPath = "catalog.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure ExcelSaveOptions to produce ODS format
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.ODS
                };

                // Save the PDF as ODS using the specified options
                pdfDoc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}