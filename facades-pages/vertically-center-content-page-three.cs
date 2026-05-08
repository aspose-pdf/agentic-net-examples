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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfPageEditor bound to the loaded document
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Target only page 3 (1‑based indexing)
                editor.ProcessPages = new int[] { 3 };

                // Align the original content vertically to the middle of the page
                editor.VerticalAlignmentType = VerticalAlignment.Center; // middle alignment

                // (Optional) you can also set horizontal alignment if needed
                // editor.HorizontalAlignment = HorizontalAlignment.Center;

                // Apply the changes to the selected page(s)
                editor.ApplyChanges();

                // Save the modified document
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Page 3 content vertically centered and saved to '{outputPath}'.");
    }
}