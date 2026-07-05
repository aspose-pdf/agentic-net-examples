using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string authorName = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Step 1: Add a text annotation (Title set to a temporary value)
        using (PdfContentEditor contentEditor = new PdfContentEditor())
        {
            contentEditor.BindPdf(inputPath);
            // Rectangle(x, y, width, height) – coordinates are in points.
            contentEditor.CreateText(
                new Rectangle(100, 500, 200, 100), // position and size
                "Temp",                           // temporary title (author placeholder)
                "This is a note added via Facades.", // annotation contents
                true,                             // open by default
                "Note",                           // icon name
                1);                               // page number (1‑based)

            contentEditor.Save(outputPath);
            contentEditor.Close();
        }

        // Step 2: Replace the temporary author with the actual author metadata
        using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
        {
            annotationEditor.BindPdf(outputPath);
            // ModifyAnnotationsAuthor(startPage, endPage, srcAuthor, desAuthor)
            annotationEditor.ModifyAnnotationsAuthor(1, 1, "Temp", authorName);
            annotationEditor.Save(outputPath);
            annotationEditor.Close();
        }

        Console.WriteLine($"Annotation added with author \"{authorName}\" and saved to '{outputPath}'.");
    }
}