using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_page3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Process only page 3 (1‑based indexing)
                editor.ProcessPages = new int[] { 3 };

                // Align the content of the selected page to the top
                editor.VerticalAlignmentType = VerticalAlignment.Top;

                // Apply the changes
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Page 3 aligned to top and saved as '{outputPath}'.");
    }
}