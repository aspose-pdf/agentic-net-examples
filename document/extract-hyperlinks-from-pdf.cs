using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "hyperlinks.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPath))
        // Open a text writer for the output list
        using (StreamWriter writer = new StreamWriter(outputPath, false))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Annotations collection is also 1‑based
                for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // We are interested only in link annotations that contain a GoToURIAction
                    if (ann is LinkAnnotation link && link.Action is GoToURIAction uriAction && !string.IsNullOrEmpty(uriAction.URI))
                    {
                        writer.WriteLine(uriAction.URI);
                    }
                }
            }
        }

        Console.WriteLine($"Hyperlinks extracted to '{outputPath}'.");
    }
}
