using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Annotations; // Added for Annotation and HighlightAnnotation

class DiffPdfGenerator
{
    static void Main()
    {
        // Input PDF files to compare
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Output PDF that will contain the visual diff
        const string diffPdfPath    = "diff.pdf";

        // Verify that the input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create the graphical comparer – it highlights visual differences.
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Verify that the default highlight color matches the documentation (red).
            if (comparer.Color != Aspose.Pdf.Color.Red)
            {
                throw new InvalidOperationException("Default highlight color is not red as documented.");
            }

            // Perform the comparison and generate the diff PDF.
            // The method uses the comparer.Color internally for the change flag.
            comparer.CompareDocumentsToPdf(doc1, doc2, diffPdfPath);
        }

        // Optional verification: load the generated diff PDF and ensure at least one annotation
        // (e.g., a highlight) uses the expected default color.
        using (Document diffDoc = new Document(diffPdfPath))
        {
            bool colorMatchFound = false;

            // Iterate through pages (1‑based indexing)
            for (int i = 1; i <= diffDoc.Pages.Count; i++)
            {
                Page page = diffDoc.Pages[i];

                // Annotations collection also uses 1‑based indexing
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];
                    // HighlightAnnotation is a subclass of MarkupAnnotation
                    if (ann is HighlightAnnotation highlight)
                    {
                        if (highlight.Color == Aspose.Pdf.Color.Red)
                        {
                            colorMatchFound = true;
                            break;
                        }
                    }
                }

                if (colorMatchFound) break;
            }

            if (!colorMatchFound)
            {
                Console.WriteLine("Warning: No red highlight annotation found in the diff PDF.");
            }
            else
            {
                Console.WriteLine("Diff PDF generated successfully with default red highlights.");
            }
        }
    }
}
