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

        // Open the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify the document has at least five pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("Document contains fewer than 5 pages.");
                return;
            }

            // Access page five
            Page page5 = doc.Pages[5];

            // Locate free‑text annotations on this page
            foreach (Annotation annot in page5.Annotations)
            {
                if (annot is FreeTextAnnotation freeText)
                {
                    // Preserve existing flags and add the Invisible flag
                    freeText.Flags = freeText.Flags | AnnotationFlags.Invisible;
                }
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Free‑text annotation on page 5 made invisible and saved to '{outputPath}'.");
    }
}