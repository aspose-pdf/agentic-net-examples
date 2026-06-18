using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonWithMetadata
{
    static void Main()
    {
        // Paths to the source PDFs and the output diff PDF
        const string sourcePdf1 = "source1.pdf";
        const string sourcePdf2 = "source2.pdf";
        const string diffPdfPath = "diff.pdf";

        // Verify that source files exist
        if (!File.Exists(sourcePdf1) || !File.Exists(sourcePdf2))
        {
            Console.Error.WriteLine("One or both source PDF files were not found.");
            return;
        }

        // Load the two source documents
        using (Document doc1 = new Document(sourcePdf1))
        using (Document doc2 = new Document(sourcePdf2))
        {
            // Perform side‑by‑side comparison and write the result to diffPdfPath
            // Using default comparison options; customize if needed
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(doc1, doc2, diffPdfPath, options);
        }

        // Re‑open the generated diff PDF to copy metadata from the first source document
        using (Document diffDoc = new Document(diffPdfPath))
        using (Document sourceDoc = new Document(sourcePdf1))
        {
            // Copy standard metadata fields
            diffDoc.Info.Title       = sourceDoc.Info.Title;
            diffDoc.Info.Author      = sourceDoc.Info.Author;
            diffDoc.Info.Subject     = sourceDoc.Info.Subject;
            diffDoc.Info.Keywords    = sourceDoc.Info.Keywords;
            diffDoc.Info.Creator     = sourceDoc.Info.Creator;
            diffDoc.Info.Producer    = sourceDoc.Info.Producer;
            diffDoc.Info.CreationDate = sourceDoc.Info.CreationDate;
            diffDoc.Info.ModDate      = sourceDoc.Info.ModDate;
            diffDoc.Info.Trapped      = sourceDoc.Info.Trapped;

            // Copy any custom metadata entries
            foreach (var entry in sourceDoc.Info)
            {
                diffDoc.Info[entry.Key] = entry.Value;
            }

            // Save the diff PDF with the transferred metadata
            diffDoc.Save(diffPdfPath);
        }

        Console.WriteLine($"Comparison completed. Diff PDF saved to '{diffPdfPath}' with source metadata preserved.");
    }
}