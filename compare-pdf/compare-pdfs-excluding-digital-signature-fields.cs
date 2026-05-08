using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string file1 = "signed1.pdf";
        const string file2 = "signed2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("One or both input files not found.");
            return;
        }

        // Load the two PDF documents
        using (Document doc1 = new Document(file1))
        using (Document doc2 = new Document(file2))
        {
            // Prepare comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Collect signature field rectangles from the first document
            List<Rectangle> excludeAreas1 = new List<Rectangle>();
            foreach (Page page in doc1.Pages)
            {
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is SignatureField sigField)
                    {
                        excludeAreas1.Add(sigField.Rect);
                    }
                }
            }

            // Collect signature field rectangles from the second document
            List<Rectangle> excludeAreas2 = new List<Rectangle>();
            foreach (Page page in doc2.Pages)
            {
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is SignatureField sigField)
                    {
                        excludeAreas2.Add(sigField.Rect);
                    }
                }
            }

            // Assign the collected rectangles to the comparison options (as arrays)
            options.ExcludeAreas1 = excludeAreas1.ToArray();
            options.ExcludeAreas2 = excludeAreas2.ToArray();

            // Perform the comparison, saving the visual result to a PDF file
            List<List<DiffOperation>> diffs = TextPdfComparer.CompareDocumentsPageByPage(
                doc1, doc2, options, resultPath);

            // Output a simple summary of differences per page
            for (int pageIndex = 0; pageIndex < diffs.Count; pageIndex++)
            {
                Console.WriteLine($"Page {pageIndex + 1}: {diffs[pageIndex].Count} change(s) detected.");
            }

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
        }
    }
}
