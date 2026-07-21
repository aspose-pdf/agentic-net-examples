using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate through each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Delete all annotations on the current page
                page.Annotations.Delete(); // AnnotationCollection.Delete() removes all annotations
            }

            // Save the cleaned PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
    }
}