using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the PdfPageEditor facade (Aspose.Pdf.Facades)
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Store the original size of page 8 (1‑based indexing)
            PageSize originalSize = editor.GetPageSize(8);

            // -----------------------------------------------------------------
            // Example modification: change page 8 size to new dimensions
            // (replace 600 and 800 with the desired width/height in points)
            // -----------------------------------------------------------------
            editor.ProcessPages = new int[] { 8 };
            editor.PageSize = new PageSize(600, 800);
            editor.ApplyChanges();

            // -----------------------------------------------------------------
            // Revert page 8 back to its original dimensions
            // -----------------------------------------------------------------
            editor.PageSize = originalSize;
            editor.ApplyChanges();

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}