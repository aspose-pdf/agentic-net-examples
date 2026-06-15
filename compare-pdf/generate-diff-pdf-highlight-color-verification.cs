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
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Load the two source documents
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Perform a text‑based comparison page by page
            ComparisonOptions options = new ComparisonOptions(); // defaults are sufficient
            var diffOperations = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Generate a PDF that highlights the differences
            PdfOutputGenerator pdfGenerator = new PdfOutputGenerator();
            pdfGenerator.GenerateOutput(diffOperations, diffPdfPath);
        }

        // Load the generated diff PDF to verify highlight color defaults
        using (Document diffDoc = new Document(diffPdfPath))
        {
            // Ensure the document has at least one page and one annotation
            if (diffDoc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Diff PDF has no pages.");
                return;
            }

            Page firstPage = diffDoc.Pages[1];
            if (firstPage.Annotations.Count == 0)
            {
                Console.Error.WriteLine("Diff PDF page contains no annotations.");
                return;
            }

            // The first annotation should be a highlight (or markup) annotation
            Annotation annotation = firstPage.Annotations[1];
            if (annotation is HighlightAnnotation highlight)
            {
                // Default highlight color for text differences is red (as per documentation)
                Aspose.Pdf.Color expectedColor = Aspose.Pdf.Color.Red;
                bool colorMatches = highlight.Color.Equals(expectedColor);
                Console.WriteLine($"Highlight color matches default: {colorMatches}");
                Console.WriteLine($"Actual color: {highlight.Color}");
            }
            else
            {
                Console.WriteLine("First annotation is not a HighlightAnnotation; cannot verify color.");
            }
        }
    }
}