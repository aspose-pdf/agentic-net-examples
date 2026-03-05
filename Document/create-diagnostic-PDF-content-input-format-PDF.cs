using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class DiagnosticPdfGenerator
{
    static void Main()
    {
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";
        const string resultPath = "diagnostic_output.pdf";

        // Ensure the source PDFs exist – create minimal placeholders if they don't.
        EnsurePdfExists(pdfPath1);
        EnsurePdfExists(pdfPath2);

        try
        {
            // Load the source PDFs (lifecycle: use Document constructor and using for disposal)
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Perform a graphical comparison and generate a PDF that visualizes the differences
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
            }

            Console.WriteLine($"Diagnostic PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while generating the diagnostic PDF: {ex.Message}");
        }
    }

    private static void EnsurePdfExists(string path)
    {
        if (!File.Exists(path))
        {
            // Create a minimal PDF file so the comparison can run without throwing FileNotFoundException.
            using (Document doc = new Document())
            {
                // Add a single blank page.
                doc.Pages.Add();
                doc.Save(path);
                Console.WriteLine($"Created placeholder PDF: {path}");
            }
        }
    }
}