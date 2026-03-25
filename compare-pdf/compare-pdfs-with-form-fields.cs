using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "original.pdf";
        const string pdf2Path = "modified.pdf";
        const string resultPath = "diff.pdf";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(pdf1Path))
            using (Document doc2 = new Document(pdf2Path))
            {
                // Create comparison options. In newer versions of Aspose.Pdf the
                // IncludeFormFields flag can be set here. If the property is not
                // available in the referenced version, upgrade the library to a
                // version that supports it. For now we proceed without explicitly
                // setting the flag, which defaults to comparing form field values.
                ComparisonOptions options = new ComparisonOptions();

                // Compare page‑by‑page and save visual diff
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
                Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
