using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        // Page number is 1‑based, annotation index is zero‑based as required by the API
        const int pageNumber = 3;
        const int annotationIndex = 2;
        const string newAuthor = "New Author";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor implements IDisposable – use a using block
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // ModifyAnnotationsAuthor has a 4‑parameter overload:
            // (int pageNumber, int annotationIndex, string author, string desAuthor)
            // If a separate destination author is not needed, reuse the same value.
            editor.ModifyAnnotationsAuthor(pageNumber, annotationIndex, newAuthor, newAuthor);

            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation author updated and saved to '{outputPath}'.");
    }
}
