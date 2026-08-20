using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to compare
        const string firstPdfPath  = "doc1.pdf";
        const string secondPdfPath = "doc2.pdf";

        // Verify that both files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Open the documents inside using blocks for deterministic disposal
        using (Aspose.Pdf.Document firstDoc  = new Aspose.Pdf.Document(firstPdfPath))
        using (Aspose.Pdf.Document secondDoc = new Aspose.Pdf.Document(secondPdfPath))
        {
            // Create default comparison options
            Aspose.Pdf.Comparison.ComparisonOptions options = new Aspose.Pdf.Comparison.ComparisonOptions();

            // Perform a flat document comparison – this returns a list of DiffOperation objects
            List<Aspose.Pdf.Comparison.DiffOperation> diffOperations =
                Aspose.Pdf.Comparison.TextPdfComparer.CompareFlatDocuments(firstDoc, secondDoc, options);

            // Iterate through the diff operations and output information.
            // Font differences are reported as separate operations (e.g., FontChange).
            foreach (Aspose.Pdf.Comparison.DiffOperation diff in diffOperations)
            {
                // The DiffOperation class exposes an 'Operation' property that indicates the type of change.
                // Use the enum name for readability.
                Console.WriteLine($"Operation: {diff.Operation}");
                
                // Additional details (such as the changed text or font name) can be obtained from the diff object.
                // Here we simply output the whole diff object which includes its ToString() representation.
                Console.WriteLine($"Details : {diff}");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}