using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for ReplaceTextStrategy
using Aspose.Pdf.Text;   // for TextState (optional)

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
            // Create and bind the PdfContentEditor facade
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure the replace‑text strategy to use regular expressions
            ReplaceTextStrategy strategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll
            };
            // Assign the strategy to the editor (property exists in the API)
            editor.ReplaceTextStrategy = strategy;

            // Regular expression to match dates in MM/DD/YYYY format
            string srcPattern = @"\b(\d{2})/(\d{2})/(\d{4})\b";
            // Replacement pattern to produce YYYY‑MM‑DD
            string destPattern = "$3-$1-$2";

            // Perform the replacement on all pages (page number 0 means all pages)
            // No specific TextState is required, so null is passed
            editor.ReplaceText(srcPattern, 0, destPattern, null);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date format conversion completed. Output saved to '{outputPath}'.");
    }
}