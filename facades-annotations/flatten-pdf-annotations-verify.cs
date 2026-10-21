using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Flatten all annotations using the Facades API.
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(inputPath);
                // Optionally delete existing annotations before flattening.
                // annotEditor.DeleteAnnotations();
                annotEditor.FlatteningAnnotations(); // correct method name
                annotEditor.Save(outputPath);
            }

            // Load the flattened document to verify that no annotation objects remain.
            using (Document doc = new Document(outputPath))
            {
                bool annotationsFound = false;

                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    if (page.Annotations != null && page.Annotations.Count > 0)
                    {
                        annotationsFound = true;
                        Console.WriteLine($"Page {i} still contains {page.Annotations.Count} annotation(s).");
                    }
                }

                if (!annotationsFound)
                {
                    Console.WriteLine("Validation passed: no annotation objects remain after flattening.");
                }
                else
                {
                    Console.WriteLine("Validation failed: some annotations were not removed.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
