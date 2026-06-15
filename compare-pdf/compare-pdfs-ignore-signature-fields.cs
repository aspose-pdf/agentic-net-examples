using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Annotation base class

class PdfSignatureAwareComparison
{
    static void Main()
    {
        const string firstPdfPath  = "first_signed.pdf";
        const string secondPdfPath = "second_signed.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Prepare comparison options and collect signature field rectangles to exclude
            ComparisonOptions options = new ComparisonOptions();

            // Exclude signature fields from the first document
            options.ExcludeAreas1 = GetSignatureFieldRectangles(doc1).ToArray();

            // Exclude signature fields from the second document
            options.ExcludeAreas2 = GetSignatureFieldRectangles(doc2).ToArray();

            // Perform the comparison; the result PDF will contain visual diff markings
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
        }
    }

    // Helper method: extracts the rectangles of all signature fields in a document
    private static List<Aspose.Pdf.Rectangle> GetSignatureFieldRectangles(Document doc)
    {
        var rects = new List<Aspose.Pdf.Rectangle>();

        // Pages are 1‑based indexed
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];

            // Annotations collection is also 1‑based
            for (int j = 1; j <= page.Annotations.Count; j++)
            {
                Annotation ann = page.Annotations[j];

                // Signature fields are represented by the SignatureField class
                if (ann is SignatureField sigField)
                {
                    // GetRectangle(true) returns the rectangle taking page rotation into account
                    Aspose.Pdf.Rectangle rect = sigField.GetRectangle(true);
                    rects.Add(rect);
                }
            }
        }

        return rects;
    }
}
