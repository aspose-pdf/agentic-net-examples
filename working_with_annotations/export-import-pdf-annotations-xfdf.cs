using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF with existing annotations
        const string xfdfFile   = "annotations.xfdf";   // temporary XFDF file
        const string outputPdf  = "roundtrip_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the original document and count its annotations
        int originalCount;
        using (Document srcDoc = new Document(inputPdf))
        {
            originalCount = CountAnnotations(srcDoc);
            Console.WriteLine($"Original annotation count: {originalCount}");

            // Export all annotations to XFDF
            srcDoc.ExportAnnotationsToXfdf(xfdfFile);
            Console.WriteLine($"Annotations exported to '{xfdfFile}'.");
        }

        // Create a fresh copy of the PDF without annotations
        using (Document cleanDoc = new Document(inputPdf))
        {
            // Remove all existing annotations from each page
            foreach (Page page in cleanDoc.Pages)
            {
                // AnnotationCollection uses 1‑based indexing
                while (page.Annotations.Count > 0)
                {
                    page.Annotations.Delete(1);
                }
            }

            // Import annotations back from the XFDF file
            cleanDoc.ImportAnnotationsFromXfdf(xfdfFile);
            Console.WriteLine("Annotations imported from XFDF.");

            // Verify that the round‑trip preserved the annotation count
            int roundTripCount = CountAnnotations(cleanDoc);
            Console.WriteLine($"Round‑trip annotation count: {roundTripCount}");

            // Save the resulting document
            cleanDoc.Save(outputPdf);
            Console.WriteLine($"Result saved to '{outputPdf}'.");
        }

        // Simple integrity check
        if (originalCount == 0)
        {
            Console.WriteLine("No annotations were present in the original document.");
        }
        else if (originalCount == CountAnnotations(new Document(outputPdf)))
        {
            Console.WriteLine("Round‑trip verification succeeded: annotation counts match.");
        }
        else
        {
            Console.WriteLine("Round‑trip verification failed: annotation counts differ.");
        }
    }

    // Helper method to count all annotations in a document
    static int CountAnnotations(Document doc)
    {
        int count = 0;
        foreach (Page page in doc.Pages)
        {
            count += page.Annotations.Count; // 1‑based collection, Count is accurate
        }
        return count;
    }
}