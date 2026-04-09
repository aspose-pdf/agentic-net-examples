using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "signed1.pdf";
        const string pdfPath2 = "signed2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files not found.");
            return;
        }

        // Load the two PDF documents
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // -----------------------------------------------------------------
            // OPTIONAL: Verify digital signatures (not required for comparison)
            // -----------------------------------------------------------------
            // SignaturesCompromiseDetector detector1 = new SignaturesCompromiseDetector(doc1);
            // detector1.Check(out var result1);
            // SignaturesCompromiseDetector detector2 = new SignaturesCompromiseDetector(doc2);
            // detector2.Check(out var result2);
            // (Result handling omitted for brevity)

            // ---------------------------------------------------------------
            // Build exclusion areas for signature fields in both documents
            // ---------------------------------------------------------------
            // The ComparisonOptions.ExcludeAreas1/ExcludeAreas2 expect an array of
            // Aspose.Pdf.Rectangle that defines regions to ignore during comparison.
            ComparisonOptions options = new ComparisonOptions();

            // Collect signature field rectangles from the first document
            var sigRects1 = GetSignatureFieldRectangles(doc1);
            if (sigRects1.Length > 0)
                options.ExcludeAreas1 = sigRects1;

            // Collect signature field rectangles from the second document
            var sigRects2 = GetSignatureFieldRectangles(doc2);
            if (sigRects2.Length > 0)
                options.ExcludeAreas2 = sigRects2;

            // ---------------------------------------------------------------
            // Perform text-based comparison, excluding the signature regions
            // ---------------------------------------------------------------
            // The result PDF will contain visual markers for differences.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }

    // Helper method: extracts the rectangles of all signature fields in a document
    private static Aspose.Pdf.Rectangle[] GetSignatureFieldRectangles(Document doc)
    {
        // The Form object holds all interactive fields.
        // Iterate through the fields and pick those of type SignatureField.
        var rectList = new System.Collections.Generic.List<Aspose.Pdf.Rectangle>();

        if (doc.Form != null && doc.Form.Fields != null)
        {
            foreach (Field field in doc.Form.Fields)
            {
                if (field is SignatureField sigField)
                {
                    // The Rect property returns the field rectangle.
                    // Use the fully qualified type to avoid ambiguity.
                    Aspose.Pdf.Rectangle rect = sigField.Rect;
                    rectList.Add(rect);
                }
            }
        }

        return rectList.ToArray();
    }
}