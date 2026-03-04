using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade namespace (required by the task)

class Program
{
    static void Main()
    {
        // Input XSL‑FO file and desired output XLSX file
        const string xslFoPath   = "input.xslfo";
        const string excelPath   = "output.xlsx";

        // Verify that the source file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Source file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO document into a PDF using XslFoLoadOptions
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();   // no external XSL required
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // Convert the PDF to XLSX using ExcelSaveOptions (Facades are not needed for conversion,
            // but the task requires using the Facades namespace, which is already referenced above)
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();

            // Save the PDF content as an Excel workbook
            pdfDoc.Save(excelPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: '{excelPath}'");
    }
}