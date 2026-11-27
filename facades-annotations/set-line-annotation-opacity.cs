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

        // Load the PDF document using the high‑level Document API (PdfContentEditor does not expose annotation methods).
        Document doc = new Document(inputPath);

        // Iterate through all pages.
        for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
        {
            var page = doc.Pages[pageNumber];
            if (page.Annotations == null) continue;

            foreach (var annot in page.Annotations)
            {
                // Process only line annotations.
                if (annot is LineAnnotation lineAnnot)
                {
                    // Set opacity to 30% (0.3).
                    lineAnnot.Opacity = 0.3f;
                }
            }
        }

        // Save the modified PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Line annotation opacity set to 0.3 and saved to '{outputPath}'.");
    }
}
