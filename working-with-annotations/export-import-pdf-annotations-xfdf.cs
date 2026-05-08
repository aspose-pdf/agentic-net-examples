using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfFile = "annotations.xfdf";
        const string outputPdf = "roundtrip_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF and count its annotations
        using (Document sourceDoc = new Document(inputPdf))
        {
            int originalCount = CountAnnotations(sourceDoc);
            Console.WriteLine($"Original annotation count: {originalCount}");

            // Export all annotations to XFDF file
            sourceDoc.ExportAnnotationsToXfdf(xfdfFile);
            Console.WriteLine($"Annotations exported to: {xfdfFile}");

            // Load a fresh copy of the same PDF for round‑trip verification
            using (Document roundTripDoc = new Document(inputPdf))
            {
                // Remove existing annotations to start from a clean state
                ClearAllAnnotations(roundTripDoc);
                Console.WriteLine("Existing annotations cleared from round‑trip document.");

                // Import annotations from the XFDF file
                roundTripDoc.ImportAnnotationsFromXfdf(xfdfFile);
                Console.WriteLine("Annotations imported from XFDF.");

                // Verify that the imported count matches the original count
                int importedCount = CountAnnotations(roundTripDoc);
                Console.WriteLine($"Imported annotation count: {importedCount}");

                if (importedCount == originalCount)
                {
                    Console.WriteLine("Round‑trip verification succeeded: counts match.");
                }
                else
                {
                    Console.WriteLine("Round‑trip verification failed: counts do not match.");
                }

                // Save the round‑trip PDF (optional)
                roundTripDoc.Save(outputPdf);
                Console.WriteLine($"Round‑trip PDF saved to: {outputPdf}");
            }
        }
    }

    // Helper method to count all annotations in a document
    static int CountAnnotations(Document doc)
    {
        int count = 0;
        for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
        {
            Page page = doc.Pages[i];
            count += page.Annotations.Count;
        }
        return count;
    }

    // Helper method to delete all annotations from a document
    static void ClearAllAnnotations(Document doc)
    {
        for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
        {
            Page page = doc.Pages[i];
            // Delete annotations in reverse order to avoid index shifting
            for (int j = page.Annotations.Count; j >= 1; j--)
            {
                page.Annotations.Delete(j);
            }
        }
    }
}