using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source XPS file and the target XLS file
        const string xpsPath = "input.xps";
        const string outputXls = "output.xlsx";

        // Verify that the source file exists
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"File not found: {xpsPath}");
            return;
        }

        // Load the XPS document and convert it to a PDF Document object
        using (Document pdfDoc = new Document(xpsPath, new XpsLoadOptions()))
        {
            // Use the PdfPageEditor facade to modify PDF pages
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(pdfDoc);

                // Example modifications:
                // Rotate all pages by 90 degrees (int literal)
                editor.Rotation = 90; // Rotation property expects an int (0, 90, 180, 270)
                // Set a zoom factor (120 = 120% i.e., 1.2) (int literal)
                editor.Zoom = 120; // Zoom property expects an int representing percentage

                // Apply the changes to the underlying PDF document
                editor.ApplyChanges();
            }

            // Save the modified PDF document as an Excel spreadsheet
            ExcelSaveOptions saveOpts = new ExcelSaveOptions();
            pdfDoc.Save(outputXls, saveOpts);
        }

        Console.WriteLine($"Conversion completed: {outputXls}");
    }
}
