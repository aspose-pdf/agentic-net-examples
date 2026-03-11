using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source XSL‑FO file and the desired XLSX output.
        const string xslFoPath = "input.xslfo";
        const string outputXlsx = "output.xlsx";

        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO file and create a PDF document in memory.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // ----- Manipulate pages using the Facades API -----
            // Example: rotate every page 90 degrees.
            PdfPageEditor pageEditor = new PdfPageEditor();
            pageEditor.BindPdf(pdfDoc);
            pageEditor.Rotation = 90;          // rotate pages
            pageEditor.ApplyChanges();         // apply changes to the bound document

            // ----- Convert the (edited) PDF to an XLSX workbook -----
            ExcelSaveOptions excelOpts = new ExcelSaveOptions(); // export options
            pdfDoc.Save(outputXlsx, excelOpts);                 // explicit SaveOptions required
        }

        Console.WriteLine($"XLSX workbook saved to '{outputXlsx}'.");
    }
}