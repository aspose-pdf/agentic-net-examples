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

        // ---------------------------------------------------------------------
        // Ensure a source PDF exists. If it does not, create a simple one that
        // contains a few dates in the MM/DD/YYYY format so the replacement can be
        // demonstrated without requiring an external file.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document sampleDoc = new Document())
            {
                Page page = sampleDoc.Pages.Add();
                // Add a paragraph with several dates to be replaced.
                page.Paragraphs.Add(new TextFragment("Report generated on 12/31/2023.\n" +
                                                    "Next review: 01/15/2024.\n" +
                                                    "Archive date: 07/04/2022."));
                sampleDoc.Save(inputPath);
                Console.WriteLine($"Sample PDF created at '{inputPath}'.");
            }
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create the content editor and bind it to the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure the editor to treat the source string as a regular expression
            ReplaceTextStrategy replaceStrategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,                     // enable regex mode
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll // replace all occurrences
            };
            // Apply the strategy to the editor (property exists in Aspose.Pdf.Facades)
            editor.ReplaceTextStrategy = replaceStrategy;

            // Regular expression to match dates in MM/DD/YYYY format
            string srcPattern = @"(\d{2})/(\d{2})/(\d{4})";
            // Replacement pattern to reorder as YYYY‑MM‑DD
            string destPattern = "$3-$1-$2";

            // Perform the replacement on all pages (page number 0 means all pages)
            // No special TextState is required, so we pass null
            editor.ReplaceText(srcPattern, 0, destPattern, null);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Date format updated and saved to '{outputPath}'.");
    }
}
