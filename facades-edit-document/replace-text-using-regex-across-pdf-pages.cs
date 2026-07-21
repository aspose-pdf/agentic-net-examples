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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create a PdfContentEditor and bind it to the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Configure the replace strategy to use regular expressions
            ReplaceTextStrategy strategy = new ReplaceTextStrategy
            {
                IsRegularExpressionUsed = true,                     // treat src pattern as regex
                ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll // replace all occurrences on all pages
            };
            editor.ReplaceTextStrategy = strategy;

            // Define the regex pattern and the replacement text
            string regexPattern   = @"\bHello\s\w+\b"; // example: matches "Hello World", "Hello Aspose", etc.
            string replacement    = "Hi";

            // Perform the replacement across all pages (page number 0 means all pages)
            // Using the overload that respects the configured ReplaceTextStrategy
            editor.ReplaceText(regexPattern, 0, replacement);

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Output saved to '{outputPath}'.");
    }
}