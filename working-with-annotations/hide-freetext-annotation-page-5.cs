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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 5 exists (pages are 1‑based)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document has fewer than 5 pages.");
                return;
            }

            Page page5 = doc.Pages[5];

            // Scan all annotations on page 5
            for (int i = 1; i <= page5.Annotations.Count; i++)
            {
                Annotation ann = page5.Annotations[i];

                // Target only FreeTextAnnotation instances
                if (ann is FreeTextAnnotation freeText)
                {
                    // Attach a HideAction that marks the annotation as hidden
                    // The second parameter (true) sets the hidden flag
                    freeText.Actions.Add(new HideAction(freeText, true));
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation on page 5 hidden; output saved to '{outputPath}'.");
    }
}