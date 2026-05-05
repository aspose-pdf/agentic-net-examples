using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the pages on which annotations should be processed
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        // Example: process pages 1, 2 and 4
        int[] pagesToProcess = { 1, 2, 4 };

        // Text to find inside annotation contents and its replacement
        const string srcText  = "Old Annotation Text";
        const string destText = "New Annotation Text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the PdfAnnotationEditor facade
            PdfAnnotationEditor editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);

            // Iterate over the specified pages
            foreach (int pageNumber in pagesToProcess)
            {
                // Ensure the page number is within the document range
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                    continue;

                Page page = doc.Pages[pageNumber];

                // Iterate over all annotations on the page (1‑based indexing)
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation annot = page.Annotations[i];

                    // Process only TextAnnotation (sticky‑note) types
                    if (annot is TextAnnotation textAnnot)
                    {
                        // If the annotation's Contents contain the source text, replace it
                        if (!string.IsNullOrEmpty(textAnnot.Contents) && textAnnot.Contents.Contains(srcText))
                        {
                            textAnnot.Contents = textAnnot.Contents.Replace(srcText, destText);
                        }
                    }
                }
            }

            // Save the modified PDF using the facade (ensures annotation changes are persisted)
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
    }
}