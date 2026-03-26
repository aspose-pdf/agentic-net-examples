using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "original.pdf";
        const string secondPdfPath = "modified.pdf";
        const string resultPdfPath = "comparison.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        using (Document firstDoc = new Document(firstPdfPath))
        using (Document secondDoc = new Document(secondPdfPath))
        {
            ComparisonOptions options = new ComparisonOptions();

            // Exclude signature fields from the first document
            options.ExcludeAreas1 = GetSignatureFieldRectangles(firstDoc).ToArray();

            // Exclude signature fields from the second document
            options.ExcludeAreas2 = GetSignatureFieldRectangles(secondDoc).ToArray();

            // Perform the comparison and save the result PDF
            TextPdfComparer.CompareDocumentsPageByPage(firstDoc, secondDoc, options, resultPdfPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }

    private static List<Rectangle> GetSignatureFieldRectangles(Document doc)
    {
        var rects = new List<Rectangle>();
        if (doc?.Form?.Fields == null)
            return rects;

        foreach (var field in doc.Form.Fields)
        {
            if (field is SignatureField sig && sig.Rect != null)
                rects.Add(sig.Rect);
        }

        return rects;
    }
}
