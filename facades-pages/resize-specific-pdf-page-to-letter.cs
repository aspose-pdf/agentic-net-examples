using System;
using System.IO;
using Aspose.Pdf.Facades;          // PdfPageEditor
using Aspose.Pdf;                 // PageSize

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_letter.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfPageEditor to edit page size
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Edit only page 6 (1‑based indexing)
            editor.ProcessPages = new int[] { 6 };

            // Set the desired output page size to Letter
            editor.PageSize = PageSize.PageLetter;

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Page 6 resized to Letter and saved as '{outputPath}'.");
    }
}