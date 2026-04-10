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

        // Existing author to replace and the new author name
        const string srcAuthor = "Old Author";
        const string desAuthor = "New Author";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, modify the author on page 3 (start = end = 3),
        // then save the updated document.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.ModifyAnnotationsAuthor(3, 3, srcAuthor, desAuthor);
            editor.Save(outputPath);
        }

        Console.WriteLine($"Author updated and saved to '{outputPath}'.");
    }
}