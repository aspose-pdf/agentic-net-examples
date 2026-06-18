using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for ReplaceTextStrategy
using Aspose.Pdf.Text;   // optional, not used directly but included for completeness

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
            // Initialize the content editor and bind it to the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure the replace text strategy to use regular expressions
            ReplaceTextStrategy replaceStrategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,
                // Replace all occurrences on all affected pages
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll
            };
            editor.ReplaceTextStrategy = replaceStrategy;

            // Regular expression to match dates in MM/DD/YYYY format
            string srcPattern = @"(\d{2})/(\d{2})/(\d{4})";
            // Replacement pattern to reorder to YYYY-MM-DD
            string destPattern = "$3-$1-$2";

            // Perform the replacement on all pages (page number 0 means all pages)
            editor.ReplaceText(srcPattern, 0, destPattern);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date format replacement completed. Output saved to '{outputPath}'.");
    }
}