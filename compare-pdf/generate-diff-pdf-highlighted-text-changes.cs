using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Comparison;

class DiffPdfGenerator
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string diffPdfPath   = "diff_output.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        // Load source documents inside using blocks (lifecycle rule)
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Comparison options – default settings are sufficient for text diff
            ComparisonOptions options = new ComparisonOptions();

            // Perform page‑by‑page text comparison (first page used as example)
            // Returns a list of DiffOperation describing insertions, deletions, etc.
            var diffOperations = TextPdfComparer.ComparePages(doc1.Pages[1], doc2.Pages[1], options);

            // Generate a PDF that visualises the text differences.
            // PdfOutputGenerator uses default highlight style (red) when no style is supplied.
            PdfOutputGenerator generator = new PdfOutputGenerator();
            generator.GenerateOutput(diffOperations, diffPdfPath);
        }

        // Load the generated diff PDF to verify the highlight colour matches the default (red)
        using (Document diffDoc = new Document(diffPdfPath))
        {
            // Assume highlights are added as HighlightAnnotation on the first page
            bool colorMatches = true;
            foreach (Annotation ann in diffDoc.Pages[1].Annotations)
            {
                if (ann is HighlightAnnotation highlight)
                {
                    // Default colour for highlights is red (Aspose.Pdf.Color.Red)
                    if (highlight.Color != Aspose.Pdf.Color.Red)
                    {
                        colorMatches = false;
                        Console.WriteLine($"Unexpected highlight colour: {highlight.Color}");
                    }
                }
            }

            Console.WriteLine(colorMatches
                ? "All highlight colours match the default (red)."
                : "One or more highlight colours do not match the default.");
        }
    }
}