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

        // Flatten all annotations using the Facades API
        using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
        {
            annotEditor.BindPdf(inputPath);
            // Correct method name – FlatteningAnnotations (not FlattenAnnotations)
            annotEditor.FlatteningAnnotations();
            annotEditor.Save(flattenedPath);
        }

        // Verify that the resulting PDF contains no annotation objects
        using (Document doc = new Document(flattenedPath))
        {
            bool hasAnnotations = false;

            // Aspose.Pdf uses 1‑based page indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                if (doc.Pages[i].Annotations != null && doc.Pages[i].Annotations.Count > 0)
                {
                    hasAnnotations = true;
                    Console.WriteLine($"Page {i} still has {doc.Pages[i].Annotations.Count} annotation(s).");
                }
            }

            if (!hasAnnotations)
                Console.WriteLine("Validation passed: No annotations found after flattening.");
            else
                Console.WriteLine("Validation failed: Some annotations remain.");
        }
    }
}
