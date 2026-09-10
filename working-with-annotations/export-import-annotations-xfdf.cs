using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Helper to count all annotations in a document
    static int GetAnnotationCount(Document doc)
    {
        int count = 0;
        foreach (Page page in doc.Pages)
        {
            count += page.Annotations.Count;
        }
        return count;
    }

    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF with annotations
        const string xfdfFile   = "annotations.xfdf";   // temporary XFDF file
        const string outputPdf  = "roundtrip_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the original document and count its annotations
            using (Document originalDoc = new Document(inputPdf))
            {
                int originalCount = GetAnnotationCount(originalDoc);
                Console.WriteLine($"Original annotation count: {originalCount}");

                // Export all annotations to XFDF
                originalDoc.ExportAnnotationsToXfdf(xfdfFile);
                Console.WriteLine($"Annotations exported to XFDF: {xfdfFile}");

                // Load the same PDF again for import test
                using (Document importDoc = new Document(inputPdf))
                {
                    // Import annotations from the XFDF file
                    importDoc.ImportAnnotationsFromXfdf(xfdfFile);
                    Console.WriteLine("Annotations imported from XFDF.");

                    // Verify round‑trip integrity by comparing counts
                    int afterImportCount = GetAnnotationCount(importDoc);
                    Console.WriteLine($"Annotation count after import: {afterImportCount}");

                    // Expected: original annotations duplicated (original * 2)
                    if (afterImportCount == originalCount * 2)
                    {
                        Console.WriteLine("Round‑trip verification succeeded.");
                    }
                    else
                    {
                        Console.WriteLine("Round‑trip verification failed.");
                    }

                    // Save the document with re‑imported annotations
                    importDoc.Save(outputPdf);
                    Console.WriteLine($"Result saved to: {outputPdf}");
                }
            }

            // Clean up temporary XFDF file
            if (File.Exists(xfdfFile))
            {
                File.Delete(xfdfFile);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}