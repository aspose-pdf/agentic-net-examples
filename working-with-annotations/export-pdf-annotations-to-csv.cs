using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string csvPath   = "annotations.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF and create a CSV writer – both wrapped in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        using (StreamWriter writer = new StreamWriter(csvPath, false))
        {
            // CSV header
            writer.WriteLine("PageNumber,AnnotationType,RectLLX,RectLLY,RectURX,RectURY,Title,Contents");

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Rectangle coordinates
                    Aspose.Pdf.Rectangle rect = ann.Rect;

                    // Annotation type name
                    string typeName = ann.GetType().Name;

                    // Title exists on markup annotations; safe cast
                    string title = string.Empty;
                    if (ann is MarkupAnnotation markup)
                        title = markup.Title ?? string.Empty;

                    // Contents may be null
                    string contents = ann.Contents ?? string.Empty;

                    // Escape double quotes for CSV compliance
                    title    = title.Replace("\"", "\"\"");
                    contents = contents.Replace("\"", "\"\"");

                    // Write CSV line
                    writer.WriteLine($"{pageIndex},{typeName},{rect.LLX},{rect.LLY},{rect.URX},{rect.URY},\"{title}\",\"{contents}\"");
                }
            }
        }

        Console.WriteLine($"Annotations exported to '{csvPath}'.");
    }
}