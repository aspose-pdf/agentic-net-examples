using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_annotations.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor, then work with its Document.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            Document doc = editor.Document;

            // Count annotations before deletion.
            int beforeCount = 0;
            for (int i = 1; i <= doc.Pages.Count; i++)
                beforeCount += doc.Pages[i].Annotations.Count;

            Console.WriteLine($"Annotations before deletion: {beforeCount}");

            // Delete all annotations in the document.
            editor.DeleteAnnotations();

            // Count annotations after deletion to verify.
            int afterCount = 0;
            for (int i = 1; i <= doc.Pages.Count; i++)
                afterCount += doc.Pages[i].Annotations.Count;

            Console.WriteLine($"Annotations after deletion: {afterCount}");

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved as '{outputPath}'.");
    }
}