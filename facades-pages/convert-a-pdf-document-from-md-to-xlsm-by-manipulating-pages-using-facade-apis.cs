using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";          // source PDF (e.g., generated from Markdown)
        const string tempPdfPath   = "temp_modified.pdf"; // intermediate PDF after page manipulation
        const string outputXlsmPath = "output.xlsm";      // final Excel macro‑enabled workbook

        // Verify source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Manipulate pages using the PdfFileEditor facade.
        // Example: delete the first page (you can adjust the page numbers).
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        int[] pagesToDelete = new int[] { 1 }; // 1‑based page numbers
        editor.Delete(inputPdfPath, pagesToDelete, tempPdfPath);

        // -----------------------------------------------------------------
        // Load the resulting PDF and convert it to an Excel workbook.
        // ExcelSaveOptions determines the Excel format; saving with a .xlsm
        // extension produces a macro‑enabled workbook.
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(tempPdfPath))
        {
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();
            pdfDoc.Save(outputXlsmPath, excelOptions);
        }

        Console.WriteLine($"Conversion completed. XLSM saved to '{outputXlsmPath}'.");
    }
}