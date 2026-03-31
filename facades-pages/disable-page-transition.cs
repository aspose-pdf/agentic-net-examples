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
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);
            editor.TransitionDuration = 0;
            editor.ProcessPages = new int[] { 6 };
            editor.ApplyChanges();

            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition disabled for page 6. Saved to '{outputPath}'.");
    }
}