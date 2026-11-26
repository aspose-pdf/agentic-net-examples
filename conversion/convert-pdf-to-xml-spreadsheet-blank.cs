using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "summary.pdf";
        const string outputPath = "summary_blank.xml";

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
                // Configure ExcelSaveOptions for XML Spreadsheet 2003 output
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Set the target format
                    Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003,
                    // Insert a blank column at the first position as required
                    InsertBlankColumnAtFirst = true
                };

                // Save the document using the options; this writes an XML Spreadsheet file
                pdfDoc.Save(outputPath, excelOpts);
            }

            Console.WriteLine($"Conversion successful: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}