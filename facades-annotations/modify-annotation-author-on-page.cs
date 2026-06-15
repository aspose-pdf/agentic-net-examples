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
        const string srcAuthor  = "Old Author"; // author to be replaced
        const string desAuthor  = "New Author"; // new author value

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfAnnotationEditor works with annotations via the Facades API
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Modify the author of annotations on page 3 (start = end = 3)
            editor.ModifyAnnotationsAuthor(3, 3, srcAuthor, desAuthor);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotation author updated and saved to '{outputPath}'.");
    }
}