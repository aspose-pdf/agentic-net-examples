using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "screen_annotations.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 4 exists (pages are 1‑based)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document has fewer than 4 pages.");
                return;
            }

            Page page = doc.Pages[4]; // page 4

            List<string> fileReferences = new List<string>();

            // Iterate over all annotations on the page and collect ScreenAnnotation file references
            foreach (Annotation annot in page.Annotations)
            {
                if (annot is ScreenAnnotation screen)
                {
                    // ScreenAnnotation does not expose a MediaFile property. The media file is supplied
                    // via the constructor and can be stored by the developer in the Title or Contents
                    // fields. We retrieve it from those fields if they are populated.
                    string reference = null;
                    if (!string.IsNullOrEmpty(screen.Title))
                        reference = screen.Title;
                    else if (!string.IsNullOrEmpty(screen.Contents))
                        reference = screen.Contents;

                    if (!string.IsNullOrEmpty(reference))
                        fileReferences.Add(reference);
                }
            }

            // Write the collected file references to a text file (one per line)
            File.WriteAllLines(outputPath, fileReferences);
        }

        Console.WriteLine($"Screen annotation file references saved to '{outputPath}'.");
    }
}
