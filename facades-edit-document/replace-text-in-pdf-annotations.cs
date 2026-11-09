using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and parameters
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        int[] pagesToProcess = { 1, 2, 3 };          // pages where annotations will be edited
        const string oldText = "Confidential";    // text to replace inside annotations
        const string newText = "Public";          // replacement text

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Iterate over the requested pages
        foreach (int pageNumber in pagesToProcess)
        {
            // Guard against out‑of‑range page numbers
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                continue;

            Page page = pdfDoc.Pages[pageNumber];

            // Iterate over all annotations on the current page
            for (int i = 0; i < page.Annotations.Count; i++)
            {
                Annotation annotation = page.Annotations[i];

                // Only annotations that expose a non‑null Contents property are relevant
                if (!string.IsNullOrEmpty(annotation.Contents) && annotation.Contents.Contains(oldText))
                {
                    // Replace the old text with the new text inside the annotation
                    annotation.Contents = annotation.Contents.Replace(oldText, newText);
                }
            }
        }

        // Save the modified PDF to the output path
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Annotation text replacement completed. Saved to '{outputPath}'.");
    }
}
