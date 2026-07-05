using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Forms;

class PdfSignatureComparison
{
    static void Main()
    {
        // Input PDF files that contain digital signatures
        const string pdfPath1 = "signed1.pdf";
        const string pdfPath2 = "signed2.pdf";

        // Path where the comparison result PDF will be saved
        const string resultPath = "comparison_result.pdf";

        // Verify that input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Prepare comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Collect signature field rectangles from the first document
            options.ExcludeAreas1 = GetSignatureRectangles(doc1).ToArray();

            // Collect signature field rectangles from the second document
            options.ExcludeAreas2 = GetSignatureRectangles(doc2).ToArray();

            // Perform the comparison, treating signature fields as unchanged regions
            // The result PDF will be written to 'resultPath'
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
        }
    }

    /// <summary>
    /// Retrieves the rectangles of all signature fields present in a PDF document.
    /// </summary>
    /// <param name="doc">The PDF document to inspect.</param>
    /// <returns>A list of Rectangle objects representing the signature field locations.</returns>
    private static List<Rectangle> GetSignatureRectangles(Document doc)
    {
        var rects = new List<Rectangle>();
        if (doc?.Form?.Fields == null)
            return rects;

        foreach (var field in doc.Form.Fields)
        {
            if (field is SignatureField sigField && sigField.Rect != null)
            {
                rects.Add(sigField.Rect);
            }
        }
        return rects;
    }
}
