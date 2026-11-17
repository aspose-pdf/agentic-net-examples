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
        const string annotationName = "YOUR_ANNOTATION_NAME"; // replace with the actual Name of the annotation

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; ensure page 2 exists
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document has fewer than two pages.");
                return;
            }

            Page pageTwo = doc.Pages[2];

            // Locate the annotation with the specified Name (unique identifier)
            Annotation target = null;
            foreach (Annotation annot in pageTwo.Annotations)
            {
                if (!string.IsNullOrEmpty(annot.Name) && annot.Name.Equals(annotationName, StringComparison.Ordinal))
                {
                    target = annot;
                    break;
                }
            }

            if (target != null)
            {
                // Remove the annotation from the page
                pageTwo.Annotations.Delete(target);
                Console.WriteLine($"Annotation with Name '{annotationName}' removed from page 2.");
            }
            else
            {
                Console.WriteLine($"Annotation with Name '{annotationName}' not found on page 2.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
