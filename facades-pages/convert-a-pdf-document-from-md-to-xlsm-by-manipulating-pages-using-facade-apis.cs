using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (generated from Markdown) and output XLSM file paths
        const string inputPdf = "input.md.pdf";
        const string tempPdf = "temp_modified.pdf";
        const string outputXlsm = "output.xlsm";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Manipulate pages using the PdfFileEditor facade API
        // ------------------------------------------------------------
        // Example: extract pages 1‑5 into a new temporary PDF.
        // Adjust the page numbers array as needed for your scenario.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Extract(inputPdf, new int[] { 1, 2, 3, 4, 5 }, tempPdf);

        // ------------------------------------------------------------
        // Load the modified PDF and convert it to XLSM (Excel macro‑enabled)
        // ------------------------------------------------------------
        using (Document doc = new Document(tempPdf))
        {
            // ExcelSaveOptions handles the conversion; the .xlsm extension
            // creates a macro‑enabled workbook.
            ExcelSaveOptions excelOpts = new ExcelSaveOptions();
            doc.Save(outputXlsm, excelOpts);
        }

        // Clean up the temporary PDF file
        try { File.Delete(tempPdf); } catch { }

        Console.WriteLine($"Conversion completed: {outputXlsm}");
    }
}