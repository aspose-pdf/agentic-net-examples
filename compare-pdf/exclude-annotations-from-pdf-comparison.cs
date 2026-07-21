using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string file1 = "doc1.pdf";
        const string file2 = "doc2.pdf";
        const string result = "comparison_result.pdf";

        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load both PDFs inside using blocks for proper disposal
        using (Aspose.Pdf.Document doc1 = new Aspose.Pdf.Document(file1))
        using (Aspose.Pdf.Document doc2 = new Aspose.Pdf.Document(file2))
        {
            // Gather annotation rectangles from the first document
            List<Aspose.Pdf.Rectangle> exclude1 = new List<Aspose.Pdf.Rectangle>();
            foreach (Aspose.Pdf.Page page in doc1.Pages)
            {
                foreach (Aspose.Pdf.Annotations.Annotation annot in page.Annotations)
                {
                    exclude1.Add(annot.Rect);
                }
            }

            // Gather annotation rectangles from the second document
            List<Aspose.Pdf.Rectangle> exclude2 = new List<Aspose.Pdf.Rectangle>();
            foreach (Aspose.Pdf.Page page in doc2.Pages)
            {
                foreach (Aspose.Pdf.Annotations.Annotation annot in page.Annotations)
                {
                    exclude2.Add(annot.Rect);
                }
            }

            // Configure comparison options to exclude the collected annotation areas
            Aspose.Pdf.Comparison.SideBySideComparisonOptions options = new Aspose.Pdf.Comparison.SideBySideComparisonOptions
            {
                ExcludeAreas1 = exclude1.ToArray(),
                ExcludeAreas2 = exclude2.ToArray()
            };

            // Perform side‑by‑side comparison and save the result
            Aspose.Pdf.Comparison.SideBySidePdfComparer.Compare(doc1, doc2, result, options);
        }

        Console.WriteLine($"Comparison saved to '{result}'.");
    }
}