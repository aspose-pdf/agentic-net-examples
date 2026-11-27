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

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Iterate over all pages (1‑based indexing used by Aspose.Pdf).
        foreach (Page page in pdfDocument.Pages)
        {
            // Walk the annotations collection backwards so that removal does not affect the loop index.
            for (int i = page.Annotations.Count; i >= 1; i--)
            {
                Annotation annotation = page.Annotations[i];

                // Use AnnotationType property to identify a Text annotation.
                if (annotation.AnnotationType == AnnotationType.Text)
                {
                    page.Annotations.Delete(i);
                }
            }
        }

        // Save the cleaned PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"All text annotations removed. Saved to '{outputPath}'.");
    }
}
