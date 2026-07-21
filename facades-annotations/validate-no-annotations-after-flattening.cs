using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        // Verify the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to a PdfAnnotationEditor and flatten all annotations.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
            {
                editor.FlatteningAnnotations(); // Removes annotation objects from the page content.
            }

            // Scan the object tree: iterate all pages and check the Annotations collection.
            bool anyAnnotationsLeft = false;
            for (int i = 1; i <= doc.Pages.Count; i++) // Pages are 1‑based.
            {
                Page page = doc.Pages[i];
                int count = page.Annotations.Count; // Annotation collection is also 1‑based.
                if (count > 0)
                {
                    anyAnnotationsLeft = true;
                    Console.WriteLine($"Page {i} still contains {count} annotation(s).");
                }
            }

            if (!anyAnnotationsLeft)
            {
                Console.WriteLine("Success: No annotation objects remain after flattening.");
            }

            // Save the flattened PDF.
            doc.Save(outputPath);
            Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
        }
    }
}