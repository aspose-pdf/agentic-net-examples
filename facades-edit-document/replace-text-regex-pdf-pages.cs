using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;   // for ReplaceTextStrategy and its Scope enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Regular expression pattern to find (example: all words starting with 'h' and ending with 'o')
        const string regexPattern   = @"h\w*?o";
        const string replacementText = "Hi";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure the editor to treat the source string as a regular expression
            // and replace all occurrences on all pages (thePage = 0 means all pages)
            editor.ReplaceTextStrategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll
            };

            // Perform the replacement while preserving the original text formatting
            // (no TextState is supplied, so existing formatting is kept)
            editor.ReplaceText(regexPattern, 0, replacementText);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}