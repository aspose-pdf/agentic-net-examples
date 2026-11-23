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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 5 pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            // Page indexing in Aspose.Pdf is 1‑based
            Page page5 = doc.Pages[5];

            // Iterate over annotations on page 5 and find the first LinkAnnotation
            foreach (Annotation annot in page5.Annotations)
            {
                if (annot is LinkAnnotation link)
                {
                    // Border for annotations requires the parent annotation in its constructor.
                    // Set the border width to 2 points (solid style).
                    link.Border = new Border(link) { Width = 2 };
                    // Optional: set border color via the annotation's Color property.
                    // link.Color = Color.Black;
                    break; // Border updated; exit the loop
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
    }
}
