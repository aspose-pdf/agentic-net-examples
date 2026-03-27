using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldAuthor = "Old Author";
        const string newAuthor = "New Author";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(inputPath);
        // Modify author of annotations on page 3 (start = 3, end = 3)
        editor.ModifyAnnotationsAuthor(3, 3, oldAuthor, newAuthor);
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Author modified and saved to '{outputPath}'.");
    }
}
