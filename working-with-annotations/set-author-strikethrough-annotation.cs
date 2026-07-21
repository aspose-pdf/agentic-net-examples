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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page (also 1‑based)
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    // Retrieve the annotation
                    Annotation ann = page.Annotations[i];

                    // Check if it is a StrikeOutAnnotation (strikethrough)
                    if (ann is StrikeOutAnnotation strike)
                    {
                        // Set the author using the Title property
                        strike.Title = "Jane Smith";
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}