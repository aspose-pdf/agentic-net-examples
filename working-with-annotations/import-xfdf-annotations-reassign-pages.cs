using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF and XFDF files
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        // Mapping: annotation Name -> target page number (1‑based)
        var annotationPageMap = new Dictionary<string, int>
        {
            { "Annot1", 2 },
            { "Annot2", 3 }
            // Add more mappings as needed
        };

        // Load the PDF document. If the source file does not exist, create a new empty document.
        Document doc;
        if (File.Exists(pdfPath))
        {
            doc = new Document(pdfPath);
        }
        else
        {
            doc = new Document();
            // Ensure the document has at least one page – required for a valid PDF structure.
            doc.Pages.Add();
        }

        // Use a using block for proper disposal of the Document instance.
        using (doc)
        {
            // Import annotations only when the XFDF file is present.
            if (File.Exists(xfdfPath))
            {
                doc.ImportAnnotationsFromXfdf(xfdfPath);
            }

            // Collect annotations that need to be moved according to the mapping.
            var moves = new List<(Annotation annotation, int targetPage)>();

            for (int i = 1; i <= doc.Pages.Count; i++) // Pages are 1‑based
            {
                Page page = doc.Pages[i];
                for (int j = 1; j <= page.Annotations.Count; j++) // Annotations are 1‑based
                {
                    Annotation ann = page.Annotations[j];
                    if (ann != null && annotationPageMap.TryGetValue(ann.Name, out int targetPage))
                    {
                        moves.Add((ann, targetPage));
                    }
                }
            }

            // Perform the moves: remove from current page and add to the target page.
            foreach (var move in moves)
            {
                // Locate and delete the annotation from its current page.
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page curPage = doc.Pages[i];
                    for (int j = 1; j <= curPage.Annotations.Count; j++)
                    {
                        if (curPage.Annotations[j] == move.annotation)
                        {
                            curPage.Annotations.Delete(j);
                            i = doc.Pages.Count + 1; // break outer loop
                            break;
                        }
                    }
                }

                // Ensure the target page exists; if not, add blank pages up to that number.
                while (doc.Pages.Count < move.targetPage)
                {
                    doc.Pages.Add();
                }

                // Add the annotation to the target page.
                Page targetPage = doc.Pages[move.targetPage];
                targetPage.Annotations.Add(move.annotation);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }
    }
}
