using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM files to be merged
        string[] cgmFiles = { "input1.cgm", "input2.cgm", "input3.cgm" };
        // Output PDF file
        const string outputPdf = "merged_output.pdf";

        // Verify that all input files exist
        foreach (var file in cgmFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Load each CGM file as a PDF Document using CgmLoadOptions
        Document[] docs = new Document[cgmFiles.Length];
        try
        {
            for (int i = 0; i < cgmFiles.Length; i++)
            {
                // CGM is an input‑only format; loading converts it to a PDF document in memory
                docs[i] = new Document(cgmFiles[i], new CgmLoadOptions());
            }

            // Merge all loaded documents into the first one
            // (Pages from subsequent documents are appended to the first document)
            for (int i = 1; i < docs.Length; i++)
            {
                docs[0].Pages.Add(docs[i].Pages);
            }

            // Save the merged result as a PDF file
            docs[0].Save(outputPdf);
            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        finally
        {
            // Ensure all Document instances are disposed to release file handles
            foreach (var doc in docs)
            {
                doc?.Dispose();
            }
        }
    }
}