using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM files to be merged
        string[] cgmFiles = { "first.cgm", "second.cgm", "third.cgm" };
        // Output PDF file
        const string outputPdf = "merged_output.pdf";

        // Verify that all input files exist
        foreach (string file in cgmFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // Load each CGM file as a PDF Document using CgmLoadOptions
        Document[] docs = new Document[cgmFiles.Length];
        for (int i = 0; i < cgmFiles.Length; i++)
        {
            // CgmLoadOptions converts the CGM into a PDF page (A4, 300dpi by default)
            docs[i] = new Document(cgmFiles[i], new CgmLoadOptions());
        }

        // Use the first document as the target and merge the rest into it
        using (Document target = docs[0])
        {
            // Merge the remaining documents (if any)
            if (docs.Length > 1)
            {
                // Create an array with the documents to merge (excluding the target)
                Document[] toMerge = new Document[docs.Length - 1];
                Array.Copy(docs, 1, toMerge, 0, toMerge.Length);
                target.Merge(toMerge);
            }

            // Save the merged result as a PDF
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}