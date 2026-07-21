using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create the content editor and bind it to the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure the editor to treat the source string as a regular expression
            ReplaceTextStrategy strategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll
            };
            editor.ReplaceTextStrategy = strategy;

            // Regular expression to match dates in MM/DD/YYYY format
            string srcPattern = @"(\d{2})/(\d{2})/(\d{4})";
            // Replacement pattern to reorder as YYYY-MM-DD
            string destPattern = "$3-$1-$2";

            // Perform replacement on all pages (thePage = 0)
            editor.ReplaceText(srcPattern, 0, destPattern);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date format conversion completed. Saved to '{outputPath}'.");
    }
}