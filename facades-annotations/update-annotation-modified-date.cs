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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF to inspect its annotations
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // If there are no annotations, simply copy the source file
            if (page.Annotations.Count == 0)
            {
                File.Copy(inputPath, outputPath, true);
                Console.WriteLine("No annotations found – file copied unchanged.");
                return;
            }

            // Retrieve the first annotation on the page
            Annotation annotation = page.Annotations[1];

            // Update its Modified date (only markup annotations expose this property)
            if (annotation is MarkupAnnotation markup)
            {
                markup.Modified = DateTime.Now;
                Console.WriteLine("Modified date updated for markup annotation.");
            }
            else
            {
                Console.WriteLine("First annotation is not a markup annotation; Modified date cannot be set.");
            }

            // Save the updated document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation ModifiedDate updated and saved to '{outputPath}'.");
    }
}
