using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Forms;

class PdfSignatureAwareComparer
{
    static void Main()
    {
        const string firstPdfPath  = "document1.pdf";
        const string secondPdfPath = "document2.pdf";
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
            // OPTIONAL: verify digital signatures are not compromised
            VerifySignatures(doc1, "Document 1");
            VerifySignatures(doc2, "Document 2");

            // Gather signature field rectangles from each document
            List<Aspose.Pdf.Rectangle> sigAreas1 = GetSignatureFieldRectangles(doc1);
            List<Aspose.Pdf.Rectangle> sigAreas2 = GetSignatureFieldRectangles(doc2);

            // Configure comparison options to exclude those areas
            ComparisonOptions options = new ComparisonOptions
            {
                ExcludeAreas1 = sigAreas1.ToArray(),
                ExcludeAreas2 = sigAreas2.ToArray()
            };

            // Perform page‑by‑page text comparison, saving the visual diff PDF
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
        }
    }

    // Extracts the rectangles of all signature fields in a document
    private static List<Aspose.Pdf.Rectangle> GetSignatureFieldRectangles(Document doc)
    {
        var rects = new List<Aspose.Pdf.Rectangle>();

        foreach (Page page in doc.Pages)
        {
            // Signature fields appear in the page's Annotations collection
            foreach (Annotation ann in page.Annotations)
            {
                if (ann is SignatureField sigField)
                {
                    // Use the Rect property (Aspose.Pdf.Rectangle) directly
                    rects.Add(sigField.Rect);
                }
            }
        }

        return rects;
    }

    // Checks for compromised signatures and reports the result
    private static void VerifySignatures(Document doc, string docName)
    {
        SignaturesCompromiseDetector detector = new SignaturesCompromiseDetector(doc);
        if (detector.Check(out var result))
        {
            Console.WriteLine($"{docName}: No compromised signatures detected.");
        }
        else
        {
            Console.WriteLine($"{docName}: Compromised signatures detected!");
            // Additional handling could be added here (e.g., logging details)
        }
    }
}
