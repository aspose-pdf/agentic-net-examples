using System;
using System.IO;
using Aspose.Pdf;

class PdfToExcelXmlConverter
{
    static void Main(string[] args)
    {
        // Input PDF file path (adjust as needed)
        string inputPdfPath = "input.pdf";

        // Output Excel XML Spreadsheet 2003 file path
        string outputXmlPath = "output.xml";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Configure Excel save options to produce Excel 2003 XML format
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.XMLSpreadSheet2003
            };

            // Save the PDF as an Excel XML Spreadsheet 2003 file
            pdfDocument.Save(outputXmlPath, saveOptions);

            Console.WriteLine($"Conversion successful. XML file saved to '{outputXmlPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}