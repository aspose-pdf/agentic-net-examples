using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "renamed_annotations.pdf";
        const string prefix     = "StandardPrefix_";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int globalCounter = 1; // Counter to ensure unique names across the whole document

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Annotations collection also uses 1‑based indexing
                for (int annIdx = 1; annIdx <= page.Annotations.Count; annIdx++)
                {
                    Annotation annotation = page.Annotations[annIdx];

                    // If the annotation already has a name, rename it; otherwise assign a new name
                    string originalName = annotation.Name;
                    string newName = string.IsNullOrEmpty(originalName)
                        ? $"{prefix}{globalCounter}"
                        : $"{prefix}{originalName}";

                    annotation.Name = newName;
                    globalCounter++;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations renamed and saved to '{outputPath}'.");
    }
}