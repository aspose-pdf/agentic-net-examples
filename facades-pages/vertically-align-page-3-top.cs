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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor facade and bind it to the loaded document
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(doc);

                // Apply changes only to page 3 (1‑based indexing)
                editor.ProcessPages = new int[] { 3 };

                // Set vertical alignment to Top for the selected page(s)
                editor.VerticalAlignmentType = VerticalAlignment.Top;

                // Apply the alignment changes
                editor.ApplyChanges();

                // Save the modified document
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Page 3 vertically aligned to top and saved as '{outputPath}'.");
    }
}