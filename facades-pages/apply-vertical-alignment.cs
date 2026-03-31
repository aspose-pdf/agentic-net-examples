using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the page editor
        var editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Specify the pages that should be aligned (1‑based page numbers)
        editor.ProcessPages = new int[] { 2, 3, 4 };

        // Set vertical alignment to the top for the selected pages
        editor.VerticalAlignmentType = Aspose.Pdf.VerticalAlignment.Top;

        // Save the modified document
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Selected pages aligned to top and saved as '{outputPath}'.");
    }
}
