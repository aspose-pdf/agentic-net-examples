using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input / output files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Text to replace inside annotations
        const string srcString = "OldText";
        const string destString = "NewText";

        // Pages on which the replacement should be performed.
        // Use 0 to indicate all pages.
        int[] pagesToProcess = new int[] { 1, 2, 0 }; // example: pages 1, 2 and all pages (0)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Bind the document to the PdfAnnotationEditor facade (required by the task)
            PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();
            annotationEditor.BindPdf(doc);

            // Determine the set of pages to iterate
            bool processAll = Array.Exists(pagesToProcess, p => p == 0);
            int startPage = 1;
            int endPage   = doc.Pages.Count;

            for (int pageNum = startPage; pageNum <= endPage; pageNum++)
            {
                // Skip pages that are not in the explicit list when not processing all pages
                if (!processAll && Array.IndexOf(pagesToProcess, pageNum) < 0)
                    continue;

                Page page = doc.Pages[pageNum];

                // Iterate over all annotations on the current page
                foreach (Annotation ann in page.Annotations)
                {
                    // Only process annotations that have textual content
                    if (!string.IsNullOrEmpty(ann.Contents) && ann.Contents.Contains(srcString))
                    {
                        // Replace the target text inside the annotation
                        ann.Contents = ann.Contents.Replace(srcString, destString);
                        // Optionally update the modification timestamp
                        ann.Modified = DateTime.Now;
                    }
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations updated and saved to '{outputPath}'.");
    }
}