using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string srcAuthor  = "Old Author"; // existing author to replace
        const string desAuthor  = "New Author"; // new author value

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor facade to modify annotation author
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Modify author on page 3 (start = end = 3)
            editor.ModifyAnnotationsAuthor(3, 3, srcAuthor, desAuthor);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation author updated and saved to '{outputPath}'.");
    }
}