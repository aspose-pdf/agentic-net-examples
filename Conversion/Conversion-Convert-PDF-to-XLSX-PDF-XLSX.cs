using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses, including ExcelSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXlsxPath = "output.xlsx";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Wrap the Document in a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // ExcelSaveOptions defaults to the XLSX format (ExcelSaveOptions.ExcelFormat.XLSX)
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();

            // Save the PDF as an Excel workbook; passing the SaveOptions ensures the correct format
            pdfDocument.Save(outputXlsxPath, excelOptions);
        }

        Console.WriteLine($"Conversion completed: '{outputXlsxPath}'");
    }
}