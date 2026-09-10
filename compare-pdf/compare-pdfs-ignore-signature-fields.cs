using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Comparison;

class PdfSignatureAwareComparer
{
    // Collect rectangles of all signature fields in a document
    private static List<Aspose.Pdf.Rectangle> GetSignatureFieldRectangles(Document doc)
    {
        var rects = new List<Aspose.Pdf.Rectangle>();

        foreach (Page page in doc.Pages)
        {
            // Signature fields are a type of annotation (also a form field)
            foreach (Annotation ann in page.Annotations)
            {
                if (ann is SignatureField sigField)
                {
                    // The rectangle is already in page coordinates
                    rects.Add(sigField.Rect);
                }
            }
        }

        return rects;
    }

    static void Main()
    {
        const string firstPdfPath  = "first_signed.pdf";
        const string secondPdfPath = "second_signed.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        try
        {
            // Load both documents
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Prepare comparison options and exclude signature field areas
                ComparisonOptions options = new ComparisonOptions();

                options.ExcludeAreas1 = GetSignatureFieldRectangles(doc1).ToArray();
                options.ExcludeAreas2 = GetSignatureFieldRectangles(doc2).ToArray();

                // Perform page‑by‑page text comparison, saving the visual diff PDF
                TextPdfComparer.CompareDocumentsPageByPage(
                    doc1,
                    doc2,
                    options,
                    resultPdfPath
                );

                Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}
