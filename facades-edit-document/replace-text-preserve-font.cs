using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF path, text to find, replacement text, and output path
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcText    = "Hello World";
        const string destText   = "Hi Universe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor (does NOT implement IDisposable)
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the editor to the loaded document
            editor.BindPdf(doc);

            // Replace the text on all pages (page number 0 means all pages)
            // This overload preserves the original font, size, and color
            editor.ReplaceText(srcText, 0, destText);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}