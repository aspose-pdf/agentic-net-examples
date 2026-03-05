using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Path for the resulting Excel 2003 XML file
        const string outputXmlPath = "output.xml";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create an instance of ExcelSaveOptions
        ExcelSaveOptions excelOptions = new ExcelSaveOptions();

        // Set the output format to Excel 2003 XML (input‑only format)
        excelOptions.Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003;

        // Load the PDF document and save it using the configured options
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            pdfDocument.Save(outputXmlPath, excelOptions);
        }

        Console.WriteLine($"PDF has been saved as Excel 2003 XML to '{outputXmlPath}'.");
    }
}