using System;
using System.IO;
using Aspose.Pdf; // All Aspose.Pdf types, including SaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure ExcelSaveOptions to produce Excel 2003 XML format
                ExcelSaveOptions saveOptions = new ExcelSaveOptions
                {
                    Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003
                };

                // Save the PDF as an XML Spreadsheet 2003 file
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Conversion successful: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}