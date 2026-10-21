using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string outputPath  = "output.pdf";
        const string searchText  = "Old Text";
        const string replaceText = "New Text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor replaces text while preserving the original font style, size, and color.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Replace all occurrences of the search text on all pages.
            // This overload keeps the original formatting of the replaced text.
            editor.ReplaceText(searchText, replaceText);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}