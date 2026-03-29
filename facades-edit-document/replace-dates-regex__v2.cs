using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
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

            string srcPattern = "(\\d{2})/(\\d{2})/(\\d{4})";
            string destPattern = "$3-$1-$2";

            // 0 means all pages
            editor.ReplaceText(srcPattern, 0, destPattern);

            doc.Save(outputPath);
        }

        Console.WriteLine("Date format replacement completed.");
    }
}