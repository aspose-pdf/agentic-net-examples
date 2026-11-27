using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Iterate over each page and remove FreeText annotations.
        foreach (Page page in doc.Pages)
        {
            // Find the indices of all FreeText annotations on this page.
            var freeTextIndices = page.Annotations
                .Select((ann, idx) => new { ann, idx })
                .Where(x => x.ann.AnnotationType == AnnotationType.FreeText)
                .Select(x => x.idx)
                .OrderByDescending(i => i) // delete from highest index to keep collection stable
                .ToList();

            // Delete the identified annotations.
            foreach (int idx in freeTextIndices)
            {
                page.Annotations.Delete(idx);
            }
        }

        // Save the cleaned PDF.
        doc.Save(outputPath);
        Console.WriteLine($"All Free Text annotations removed. Saved to '{outputPath}'.");
    }
}
