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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure the editor to treat the source string as a regular expression
            ReplaceTextStrategy strategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,
                // Replace all occurrences on all pages
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll
            };
            editor.ReplaceTextStrategy = strategy;

            // Regular expression to match dates in MM/DD/YYYY format
            string srcPattern = @"(\d{2})/(\d{2})/(\d{4})";
            // Desired format: YYYY-MM-DD using captured groups
            string destPattern = "$3-$1-$2";

            // Perform the replacement on all pages (page index 0 means all pages)
            // No specific TextState is required, so we pass null
            editor.ReplaceText(srcPattern, 0, destPattern, null);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date format conversion completed. Output saved to '{outputPath}'.");
    }
}