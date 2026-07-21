using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string diffPdfPath   = "diff_output.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        // Load the two documents to be compared
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Default comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison
            List<List<DiffOperation>> diffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Generate a PDF that visualises the differences (highlights)
            PdfOutputGenerator generator = new PdfOutputGenerator();
            generator.GenerateOutput(diffs, diffPdfPath);
        }

        // Verify that the highlight colour used matches the default (Yellow)
        if (!File.Exists(diffPdfPath))
        {
            Console.Error.WriteLine("Diff PDF was not created.");
            return;
        }

        using (Document diffDoc = new Document(diffPdfPath))
        {
            bool allHighlightsMatch = true;

            foreach (Page page in diffDoc.Pages)
            {
                foreach (Aspose.Pdf.Annotations.Annotation ann in page.Annotations)
                {
                    if (ann is Aspose.Pdf.Annotations.HighlightAnnotation highlight)
                    {
                        // The default highlight colour for PdfOutputGenerator is Yellow
                        if (highlight.Color != Aspose.Pdf.Color.Yellow)
                        {
                            allHighlightsMatch = false;
                            Console.WriteLine($"Page {page.Number}: Highlight colour differs (found {highlight.Color}).");
                        }
                    }
                }
            }

            Console.WriteLine(allHighlightsMatch
                ? "All highlight colours match the default (Yellow)."
                : "One or more highlight colours do not match the default.");
        }
    }
}