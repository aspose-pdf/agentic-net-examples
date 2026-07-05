using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "aligned_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor (Facade) to modify page alignment.
        // The facade implements IDisposable, so wrap it in a using block.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Specify that only page 3 should be processed.
            editor.ProcessPages = new int[] { 3 };

            // Align the original content of the page to the top.
            editor.VerticalAlignmentType = VerticalAlignment.Top;

            // Apply the changes to the document.
            editor.ApplyChanges();

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 3 aligned to top and saved as '{outputPath}'.");
    }
}