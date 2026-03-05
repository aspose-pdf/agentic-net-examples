using System;
using System.IO;
using Aspose.Pdf; // Core PDF API and ExcelSaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF
        const string outputXml = "output.xml"; // Excel 2003 XML spreadsheet

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure ExcelSaveOptions to produce Excel 2003 XML format
            ExcelSaveOptions saveOpts = new ExcelSaveOptions
            {
                // The enum value XMLSpreadSheet2003 corresponds to the 2003 XML spreadsheet format
                Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003
            };

            // Save the PDF as an Excel 2003 XML spreadsheet
            pdfDoc.Save(outputXml, saveOpts);
        }

        Console.WriteLine($"PDF successfully converted to Excel 2003 XML: {outputXml}");
    }
}