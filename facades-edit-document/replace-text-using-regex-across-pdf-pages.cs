using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for PdfContentEditor and ReplaceTextStrategy

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Regular expression pattern to find (example: any word starting with "test")
        const string pattern     = @"\btest\w*\b";
        // Replacement text
        const string replacement = "sample";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended using block for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create the Facades editor and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure the replace strategy to treat the source string as a regular expression
            ReplaceTextStrategy strategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,
                // Replace all occurrences on all pages (thePage = 0 means all pages)
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll
            };
            editor.ReplaceTextStrategy = strategy;

            // Perform the replacement across all pages while preserving original formatting
            editor.ReplaceText(pattern, replacement);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}