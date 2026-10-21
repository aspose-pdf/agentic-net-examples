using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and pages on which annotations should be edited
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        int[] pagesToEdit = { 1, 3, 5 }; // example page numbers (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Facade instance – required by the task specification
        PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();

        // Load the document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate only the specified pages
            foreach (int pageNumber in pagesToEdit)
            {
                // Guard against invalid page numbers
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    continue;

                Page page = doc.Pages[pageNumber];

                // Iterate all annotations on the current page
                for (int i = 0; i < page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Process only text annotations (pop‑up notes)
                    if (ann is TextAnnotation textAnn)
                    {
                        // Example: replace the whole content or a substring
                        string original = textAnn.Contents ?? string.Empty;
                        string replaced = original.Replace("old text", "new text");
                        textAnn.Contents = replaced;
                    }
                }
            }

            // Save the modified PDF (lifecycle rule – save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
    }
}