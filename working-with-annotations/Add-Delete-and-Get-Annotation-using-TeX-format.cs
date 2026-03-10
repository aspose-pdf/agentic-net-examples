using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // for TeXFragment (inherits FormattedFragment)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Add a TextAnnotation whose content is a TeX fragment
            // -------------------------------------------------
            // The raw TeX string that we want to store in the annotation
            const string texString = @"\\frac{a}{b}"; // note the double back‑slashes for C# string literal

            // Create a TeXFragment – this object can be used later for rendering if needed
            TeXFragment tex = new TeXFragment(texString);

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the annotation on the first page
            TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "LaTeX Note",
                // Use the original TeX string as the annotation's visible content
                Contents = texString,
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(annotation);

            // -------------------------------------------------
            // 2. Get (retrieve) the annotation we just added
            // -------------------------------------------------
            // Annotations collection uses 1‑based indexing
            Annotation retrieved = doc.Pages[1].Annotations[1];
            Console.WriteLine($"Retrieved Annotation Title: {(retrieved is TextAnnotation ta ? ta.Title : "N/A")}");
            Console.WriteLine($"Retrieved Annotation Contents: {retrieved.Contents}");

            // -------------------------------------------------
            // 3. Delete the annotation
            // -------------------------------------------------
            // Delete by index (1‑based). After deletion the collection will be empty.
            doc.Pages[1].Annotations.Delete(1);

            // -------------------------------------------------
            // Save the modified document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
