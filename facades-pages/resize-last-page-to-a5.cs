using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the PDF page editor facade
        PdfPageEditor editor = new PdfPageEditor();

        // Bind the source PDF file
        editor.BindPdf(inputPath);

        // Determine the total number of pages (1‑based indexing)
        int totalPages = editor.GetPages();

        // Target only the last page for resizing
        editor.ProcessPages = new int[] { totalPages };

        // Set the desired page size (A5)
        editor.PageSize = PageSize.A5;

        // Apply the changes to the document
        editor.ApplyChanges();

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources
        editor.Close();

        Console.WriteLine($"Last page resized to A5 and saved as '{outputPath}'.");
    }
}