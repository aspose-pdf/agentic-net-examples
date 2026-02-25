using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf  = "doc1.pdf";
        const string secondPdf = "doc2.pdf";
        const string resultPdf = "comparison_result.pdf";

        // Verify that both source files exist before proceeding.
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Wrap both Document instances in using blocks for deterministic disposal.
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // Create the graphical comparer.
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize comparer settings.
                // comparer.Color = System.Drawing.Color.Red; // default is red
                // comparer.Threshold = 0; // percentage of ignored changes

                // Perform the comparison and write the result directly to a PDF file.
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdf);
            }

            Console.WriteLine($"Graphical comparison saved to '{resultPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}