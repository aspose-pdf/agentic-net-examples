using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_annotations.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the annotation types we want to extract: FreeText and Text
        AnnotationType[] targetTypes = new[]
        {
            AnnotationType.FreeText,
            AnnotationType.Text
        };

        // Load the document to access page annotations
        using (Document doc = new Document(inputPath))
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Access annotations directly from the page object
                Page page = doc.Pages[pageNum];
                foreach (Annotation ann in page.Annotations)
                {
                    // Filter only the desired annotation types
                    if (Array.IndexOf(targetTypes, ann.AnnotationType) >= 0)
                    {
                        writer.WriteLine($"Page {pageNum}, Type: {ann.AnnotationType}, Contents: {ann.Contents}");
                    }
                }
            }
        }

        Console.WriteLine($"Selected annotations have been saved to '{outputPath}'.");
    }
}
