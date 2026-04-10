using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF
        const string outputPath = "output.pdf";  // result PDF
        const string srcString  = "OldText";    // text to find inside annotations
        const string destString = "NewText";    // replacement text

        // pages on which annotation text should be processed (1‑based indexing)
        int[] targetPages = { 1, 2, 3 };

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document – always wrap in a using block (document‑disposal rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over the specified pages
            foreach (int pageNum in targetPages)
            {
                if (pageNum < 1 || pageNum > doc.Pages.Count)
                    continue; // skip invalid page numbers

                Page page = doc.Pages[pageNum];

                // Examine each annotation on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Only process annotations that have textual content
                    if (!string.IsNullOrEmpty(ann.Contents) && ann.Contents.Contains(srcString))
                    {
                        // Replace the target substring inside the annotation's Contents
                        ann.Contents = ann.Contents.Replace(srcString, destString);
                    }
                }
            }

            // Use the Facade API (PdfAnnotationEditor) to save the modified document.
            // Binding the Document to the editor satisfies the “use Aspose.Pdf.Facades” requirement.
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);          // initialize the facade with the edited document
                editor.Save(outputPath);      // persist changes
            }
        }

        Console.WriteLine($"Annotation text replacement completed. Saved to '{outputPath}'.");
    }
}