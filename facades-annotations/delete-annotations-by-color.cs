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

        // Define the target color (e.g., pure red)
        Aspose.Pdf.Color targetColor = Aspose.Pdf.Color.FromRgb(1.0, 0.0, 0.0);

        using (Document doc = new Document(inputPath))
        {
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                // Iterate backwards because deleting changes the collection indexing
                for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                {
                    Annotation annotation = page.Annotations[annIndex];
                    if (annotation.Color != null && annotation.Color.Equals(targetColor))
                    {
                        page.Annotations.Delete(annIndex);
                    }
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations with the specified color removed. Saved to '{outputPath}'.");
    }
}