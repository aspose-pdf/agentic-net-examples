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
            bool replaced = editor.ReplaceText("Draft", "Final");
            if (!replaced)
            {
                Console.WriteLine("No occurrences of 'Draft' were found.");
            }
            doc.Save(outputPath);
        }

        Console.WriteLine($"Replaced 'Draft' with 'Final' and saved to '{outputPath}'.");
    }
}
