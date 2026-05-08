using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcAuthor  = "Old Author";
        const string newAuthor  = "New Author";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Modify the author of annotations on page 3 (1‑based indexing)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.ModifyAnnotationsAuthor(3, 3, srcAuthor, newAuthor);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations author updated and saved to '{outputPath}'.");
    }
}