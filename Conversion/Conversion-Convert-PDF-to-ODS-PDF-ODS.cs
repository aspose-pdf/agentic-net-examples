using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputOds = "output.ods";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure ExcelSaveOptions to produce ODS format
                ExcelSaveOptions saveOpts = new ExcelSaveOptions();
                saveOpts.Format = ExcelSaveOptions.ExcelFormat.ODS; // ODS = OpenDocument Spreadsheet

                // Save the document as ODS using explicit SaveOptions
                pdfDoc.Save(outputOds, saveOpts);
            }

            Console.WriteLine($"Conversion successful: '{outputOds}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}