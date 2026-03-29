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
        const string regexPattern = "Invoice\\s+\\d{4}"; // example pattern
        const string replacement = "Invoice ****";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // 0 means "all pages"; this overload preserves original formatting
            editor.ReplaceText(regexPattern, 0, replacement);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replacement completed. Saved to '{outputPath}'.");
    }
}
