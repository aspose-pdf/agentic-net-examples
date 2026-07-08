using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for ReplaceTextStrategy
// Note: No System.Drawing usage – use Aspose.Pdf.Color only if needed.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Regular expression pattern to find (example: any word starting with "foo")
        const string regexPattern = @"\bfoo\w*\b";
        // Replacement text (preserves original formatting)
        const string replacement   = "bar";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (creation rule)
        using (Document doc = new Document(inputPath))
        {
            // Create and bind the PdfContentEditor (facade API)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure replace‑text strategy to use regular expressions and replace all occurrences
            ReplaceTextStrategy strategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll
            };
            editor.ReplaceTextStrategy = strategy;

            // Perform the replacement across all pages (thePage = 0 means all pages)
            // No TextState is supplied, so original font, size and color are kept.
            editor.ReplaceText(regexPattern, 0, replacement);

            // Save the modified document (save rule – using explicit path)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}