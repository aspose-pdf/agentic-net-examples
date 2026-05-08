using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPath = "input.pdf";
        // Output PDF file with applied vertical alignment
        const string outputPath = "aligned_output.pdf";

        // Pages to which the vertical alignment will be applied (1‑based indexing)
        int[] targetPages = new int[] { 1, 2, 3 }; // adjust as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source document and edit its pages using PdfPageEditor
        using (Document doc = new Document(inputPath))
        using (PdfPageEditor editor = new PdfPageEditor(doc))
        {
            // Specify the pages to be processed
            editor.ProcessPages = targetPages;

            // Apply top vertical alignment to the original content on the result pages
            editor.VerticalAlignmentType = VerticalAlignment.Top;

            // Apply the changes to the selected pages
            editor.ApplyChanges();

            // Save the edited document
            editor.Save(outputPath);
        }

        Console.WriteLine($"Vertical alignment applied and saved to '{outputPath}'.");
    }
}