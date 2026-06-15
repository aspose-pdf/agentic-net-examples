using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string annotationName = "YOUR_ANNOTATION_NAME"; // replace with the actual annotation Name (unique identifier)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages (pages are 1‑based)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document contains fewer than 2 pages.");
                return;
            }

            // Get page 2
            Page page = doc.Pages[2];

            // Locate the annotation with the specified Name (used as a unique identifier)
            Annotation target = null;
            foreach (Annotation ann in page.Annotations)
            {
                // The Annotation class does not expose an Id property; use Name instead.
                if (!string.IsNullOrEmpty(ann.Name) && ann.Name.Equals(annotationName, StringComparison.Ordinal))
                {
                    target = ann;
                    break;
                }
            }

            if (target != null)
            {
                // Delete the found annotation from the collection
                page.Annotations.Delete(target);
                Console.WriteLine($"Deleted annotation Name '{annotationName}' from page 2.");
            }
            else
            {
                Console.WriteLine($"Annotation Name '{annotationName}' not found on page 2.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
