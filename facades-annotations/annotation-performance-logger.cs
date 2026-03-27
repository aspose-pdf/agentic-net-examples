using System;
using System.IO;
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class AnnotationPerformanceLogger
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // ----- Add a TextAnnotation and measure duration -----
                Stopwatch addTimer = Stopwatch.StartNew();
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation textAnn = new TextAnnotation(page, rect);
                textAnn.Title = "Note";
                textAnn.Contents = $"Added on page {i}";
                textAnn.Color = Aspose.Pdf.Color.Yellow;
                page.Annotations.Add(textAnn);
                addTimer.Stop();
                Console.WriteLine($"Add TextAnnotation on page {i} took {addTimer.ElapsedMilliseconds} ms");

                // ----- Delete the just‑added annotation and measure duration -----
                Stopwatch deleteTimer = Stopwatch.StartNew();
                // Annotations collection uses 1‑based indexing as well
                page.Annotations.Delete(page.Annotations.Count);
                deleteTimer.Stop();
                Console.WriteLine($"Delete last annotation on page {i} took {deleteTimer.ElapsedMilliseconds} ms");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved as '{outputPath}'.");
    }
}