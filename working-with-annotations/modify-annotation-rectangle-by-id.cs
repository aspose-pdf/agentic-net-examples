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
        const string targetName = "Annot123"; // Name of the annotation to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            Annotation foundAnnotation = null;

            // Iterate through all pages (1‑based indexing) to find the annotation with the given Name
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                foreach (Annotation annot in page.Annotations)
                {
                    // The base Annotation class does not expose an Id property.
                    // Use the Name property (or FullName) to locate a specific annotation.
                    if (!string.IsNullOrEmpty(annot.Name) &&
                        annot.Name.Equals(targetName, StringComparison.Ordinal))
                    {
                        foundAnnotation = annot;
                        break;
                    }
                }
                if (foundAnnotation != null) break;
            }

            if (foundAnnotation == null)
            {
                Console.Error.WriteLine($"Annotation with Name '{targetName}' not found.");
                return;
            }

            // Modify the rectangle (position and size) of the found annotation.
            // Example: move to (100, 500) lower‑left and (300, 550) upper‑right.
            foundAnnotation.Rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation '{targetName}' updated and saved to '{outputPath}'.");
    }
}
