using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for source XPS and destination XLS files
        const string xpsPath = "input.xps";
        const string xlsPath = "output.xlsx";

        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"Source file not found: {xpsPath}");
            return;
        }

        // Load the XPS file into a PDF Document (conversion happens on load)
        using (Document pdfDoc = new Document(xpsPath, new XpsLoadOptions()))
        {
            // ----- Modify PDF pages using the PdfPageEditor facade -----
            // Example: rotate every page 90 degrees and set a uniform page size
            using (PdfPageEditor pageEditor = new PdfPageEditor(pdfDoc))
            {
                // Apply the same rotation to all pages
                pageEditor.Rotation = 90;               // valid values: 0, 90, 180, 270
                // Optional: change page size (e.g., A4)
                pageEditor.PageSize = PageSize.A4;
                // Apply the changes to the document
                pageEditor.ApplyChanges();
            }

            // ----- Convert the (now modified) PDF document to an Excel spreadsheet -----
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();
            // Save the document as XLSX (ExcelSaveOptions determines the format)
            pdfDoc.Save(xlsPath, excelOptions);
        }

        Console.WriteLine($"XPS converted to XLS successfully: {xlsPath}");
    }
}