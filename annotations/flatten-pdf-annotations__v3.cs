using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "AnnotationsFlattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf uses 1‑based page numbers)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate all annotations on the current page
                foreach (Annotation annotation in page.Annotations)
                {
                    // Flatten the annotation into the page content
                    annotation.Flatten();
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations flattened and saved to '{outputPath}'.");
    }
}