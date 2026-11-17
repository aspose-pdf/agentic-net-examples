using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page
            Page page = doc.Pages[1];

            // Use a TextFragmentAbsorber to locate the first piece of text (approximate first paragraph)
            TextFragmentAbsorber absorber = new TextFragmentAbsorber();
            page.Accept(absorber);
            var fragments = absorber.TextFragments;

            if (fragments == null || fragments.Count == 0)
            {
                Console.WriteLine("No text found on the first page.");
                doc.Save(outputPath);
                return;
            }

            // Get the rectangle that bounds the first text fragment
            Aspose.Pdf.Rectangle rect = fragments[0].Rectangle;

            // Create a strike‑out annotation over the identified rectangle
            StrikeOutAnnotation strikeOut = new StrikeOutAnnotation(page, rect)
            {
                // Set the author of the annotation
                Title = "reviewer"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(strikeOut);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strikethrough annotation added and saved to '{outputPath}'.");
    }
}
