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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block (ensures proper disposal)
        using (Document srcDoc = new Document(inputPath))
        {
            // Create an empty destination document that will receive the content with the page break
            using (Document destDoc = new Document())
            {
                // Define a page break:
                // - PageNumber is 1‑based (the page where the break will be inserted)
                // - Position is the vertical coordinate from the bottom of the page (default units = points)
                var pageBreak = new PdfFileEditor.PageBreak(pageNumber: 1, position: 500);

                // Instantiate the facade that provides the AddPageBreak operation
                PdfFileEditor editor = new PdfFileEditor();

                // Add the page break. This copies all existing pages from srcDoc to destDoc
                // and inserts the specified break(s) while preserving the original content.
                editor.AddPageBreak(srcDoc, destDoc, new PdfFileEditor.PageBreak[] { pageBreak });

                // Save the resulting PDF. The Save method writes a PDF regardless of file extension.
                destDoc.Save(outputPath);
            }
        }

        Console.WriteLine($"Page break added successfully. Output saved to '{outputPath}'.");
    }
}