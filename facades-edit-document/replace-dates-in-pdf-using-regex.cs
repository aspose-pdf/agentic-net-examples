using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for ReplaceTextStrategy
using Aspose.Pdf.Text;   // optional, not used directly

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create and bind the PdfContentEditor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure the editor to use regular expressions and replace all occurrences
            ReplaceTextStrategy strategy = new ReplaceTextStrategy {
                IsRegularExpressionUsed = true,
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll
            };
            editor.ReplaceTextStrategy = strategy;

            // Regular expression to match dates in MM/DD/YYYY format
            string srcPattern = @"(\d{2})/(\d{2})/(\d{4})";
            // Replacement pattern to produce YYYY-MM-DD
            string destPattern = "$3-$1-$2";

            // Replace on all pages (page number 0 means all pages)
            editor.ReplaceText(srcPattern, 0, destPattern);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date format updated and saved to '{outputPath}'.");
    }
}