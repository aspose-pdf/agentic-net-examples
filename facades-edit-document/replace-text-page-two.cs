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
        const string searchText = "OldString";
        const string replaceText = "NewString";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);
            // Replace text on page 2 while preserving original font style
            editor.ReplaceText(searchText, 2, replaceText);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced text on page 2 and saved to '{outputPath}'.");
    }
}