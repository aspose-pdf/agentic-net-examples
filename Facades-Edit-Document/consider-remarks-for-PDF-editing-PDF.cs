using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Text to search for and its replacement
        const string searchText = "Hello";
        const string replaceText = "Hi";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (a Facade) to edit the PDF
        // The facade implements IDisposable, so wrap it in a using block
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the PDF file to the editor
            editor.BindPdf(inputPath);

            // Replace all occurrences of the search text with the replacement text
            editor.ReplaceText(searchText, replaceText);

            // Save the modified PDF to the output path
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}