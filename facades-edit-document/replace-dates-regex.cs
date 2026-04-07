using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            ReplaceTextStrategy strategy = new ReplaceTextStrategy();
            strategy.IsRegularExpressionUsed = true;
            strategy.ReplaceScope = ReplaceTextStrategy.Scope.ReplaceAll;
            editor.ReplaceTextStrategy = strategy;

            string pattern = @"(\d{2})/(\d{2})/(\d{4})"; // MM/DD/YYYY
            string replacement = "$3-$1-$2"; // YYYY-MM-DD

            // 0 means all pages
            editor.ReplaceText(pattern, 0, replacement);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Dates reformatted and saved to '{outputPath}'.");
    }
}