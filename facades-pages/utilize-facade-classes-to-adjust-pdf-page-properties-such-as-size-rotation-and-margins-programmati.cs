using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string intermediatePath = "intermediate.pdf";
        const string finalOutputPath = "final_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Adjust page size and rotation using PdfPageEditor (facade)
        // ------------------------------------------------------------
        // PdfPageEditor inherits from SaveableFacade and implements IDisposable,
        // so we wrap it in a using block to ensure proper resource cleanup.
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Load the source PDF file into the facade.
            pageEditor.BindPdf(inputPath);

            // Set the output page size. PageSize enum is defined in Aspose.Pdf.
            // Example: A4 size (595 x 842 points).
            pageEditor.PageSize = PageSize.A4;

            // Rotate all pages by 90 degrees clockwise.
            // Valid values: 0, 90, 180, 270.
            pageEditor.Rotation = 90;

            // Process all pages (null means the whole document).
            pageEditor.ProcessPages = null;

            // Apply the configured changes to the document.
            pageEditor.ApplyChanges();

            // Save the modified PDF to an intermediate file.
            pageEditor.Save(intermediatePath);
        }

        // ------------------------------------------------------------
        // 2. Add uniform margins to all pages using PdfFileEditor (facade)
        // ------------------------------------------------------------
        // PdfFileEditor does NOT implement IDisposable, so no using block is needed.
        PdfFileEditor fileEditor = new PdfFileEditor();

        // AddMargins resizes page contents and adds the specified margins.
        // Parameters: source file, destination file, pages array (null = all),
        // left, right, top, bottom margins (in default PDF units, 1/72 inch).
        bool marginsAdded = fileEditor.AddMargins(
            intermediatePath,          // source PDF
            finalOutputPath,           // destination PDF
            null,                      // process all pages
            20,                        // left margin
            20,                        // right margin
            20,                        // top margin
            20);                       // bottom margin

        if (!marginsAdded)
        {
            Console.Error.WriteLine("Failed to add margins to the PDF.");
            return;
        }

        Console.WriteLine($"PDF processing complete. Result saved to '{finalOutputPath}'.");
    }
}