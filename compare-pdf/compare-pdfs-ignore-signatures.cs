using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string file1 = "original.pdf";
        const string file2 = "signed.pdf";
        const string result = "comparison_result.pdf";

        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        using (Document doc1 = new Document(file1))
        using (Document doc2 = new Document(file2))
        {
            // Gather signature field rectangles from each document
            List<Rectangle> sigRects1 = GetSignatureRectangles(doc1);
            List<Rectangle> sigRects2 = GetSignatureRectangles(doc2);

            // Set comparison options to exclude those areas
            ComparisonOptions options = new ComparisonOptions
            {
                ExcludeAreas1 = sigRects1.ToArray(),
                ExcludeAreas2 = sigRects2.ToArray()
            };

            // Perform comparison and save the visual result
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, result);
            Console.WriteLine($"Comparison saved to '{result}'.");
        }
    }

    static List<Rectangle> GetSignatureRectangles(Document doc)
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
