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
        const string annotationName = "unique-id-to-delete"; // the identifier stored in the Name property

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page two exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("Document has less than two pages.");
                return;
            }

            // Retrieve page two
            Page pageTwo = doc.Pages[2];

            // Locate the annotation with the specified Name (unique identifier)
            Annotation? target = null;
            foreach (Annotation annot in pageTwo.Annotations)
            {
                if (!string.IsNullOrEmpty(annot.Name) &&
                    annot.Name.Equals(annotationName, StringComparison.Ordinal))
                {
                    target = annot;
                    break;
                }
            }

            if (target != null)
            {
                // Remove the annotation from the page's annotation collection
                pageTwo.Annotations.Remove(target);
                Console.WriteLine($"Annotation with Name '{annotationName}' removed from page 2.");
            }
            else
            {
                Console.WriteLine($"Annotation with Name '{annotationName}' not found on page 2.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
