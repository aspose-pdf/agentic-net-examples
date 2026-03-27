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

        // Load the PDF and count existing annotations
        using (Document doc = new Document(inputPath))
        {
            int initialCount = 0;
            foreach (Page page in doc.Pages)
            {
                initialCount += page.Annotations.Count;
            }
            Console.WriteLine($"Initial annotation count: {initialCount}");

            // Delete all annotations using PdfAnnotationEditor
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);
            editor.DeleteAnnotations();
            editor.Save(outputPath);
            editor.Close();

            // Count annotations after deletion
            int afterCount = 0;
            foreach (Page page in doc.Pages)
            {
                afterCount += page.Annotations.Count;
            }
            Console.WriteLine($"Annotation count after deletion: {afterCount}");
        }
    }
}
