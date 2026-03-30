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
        const string searchText = "Hello World";
        const string replaceText = "Hi Universe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Replace on all pages (page number 0) while preserving original font, size, and color
            editor.ReplaceText(searchText, 0, replaceText);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Text replaced and saved to '{outputPath}'.");
    }
}