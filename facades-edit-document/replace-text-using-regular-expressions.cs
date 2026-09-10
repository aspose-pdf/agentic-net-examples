using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string pattern    = @"\bHello\s\w+\b";   // regular expression to match
        const string replacement = "Hi there";

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

            // Configure the replace strategy to use regular expressions and replace all occurrences
            ReplaceTextStrategy strategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll
            };
            editor.ReplaceTextStrategy = strategy;

            // Perform the replacement on all pages (page number 0 means all pages)
            // No TextState is supplied, so original font, size, color, etc. are preserved
            editor.ReplaceText(pattern, 0, replacement);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}