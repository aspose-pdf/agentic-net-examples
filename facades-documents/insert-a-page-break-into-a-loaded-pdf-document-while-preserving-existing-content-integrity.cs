using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_break.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Define where the page break should be inserted:
        //   - PageNumber: 2 (pages are 1‑based)
        //   - Position:   400 points from the bottom of the page
        var breakInfo = new PdfFileEditor.PageBreak(2, 400);
        var pageBreaks = new PdfFileEditor.PageBreak[] { breakInfo };

        // Load the source PDF document
        using (Document srcDoc = new Document(inputPath))
        {
            // Create an empty destination document
            using (Document destDoc = new Document())
            {
                // Facade that performs the page‑break operation
                var editor = new PdfFileEditor();

                // Insert the page break(s) from srcDoc into destDoc
                editor.AddPageBreak(srcDoc, destDoc, pageBreaks);

                // Persist the result
                destDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Page break added successfully. Output saved to '{outputPath}'.");
    }
}