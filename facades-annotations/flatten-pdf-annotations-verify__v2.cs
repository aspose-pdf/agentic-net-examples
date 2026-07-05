using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string flattenedPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Flatten all annotations using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);
            editor.FlatteningAnnotations(); // removes annotation objects
            editor.Save(flattenedPath);
        }

        // Verify that no annotation objects remain after flattening
        using (Document doc = new Document(flattenedPath))
        {
            bool anyAnnotations = false;
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                if (page.Annotations.Count > 0)
                {
                    anyAnnotations = true;
                    Console.WriteLine($"Page {i} still contains {page.Annotations.Count} annotation(s).");
                }
            }

            if (!anyAnnotations)
            {
                Console.WriteLine("All annotations have been successfully flattened; none remain.");
            }
        }
    }
}