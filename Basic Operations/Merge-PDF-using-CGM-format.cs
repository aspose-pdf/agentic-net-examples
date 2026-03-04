using System;
using System.IO;
using Aspose.Pdf;   // Contains Document, CgmLoadOptions, etc.

class Program
{
    static void Main()
    {
        // Input CGM files to be merged
        string[] cgmFiles = { "input1.cgm", "input2.cgm", "input3.cgm" };
        const string outputPdf = "merged_output.pdf";

        // Verify that all input files exist
        foreach (string file in cgmFiles)
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
                docs[i] = new Document(cgmFiles[i], new CgmLoadOptions());
            }

            // Merge all documents into the first one
            docs[0].Merge(docs);

            // Save the merged result as a PDF
            docs[0].Save(outputPdf);
            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        finally
        {
            // Ensure all Document instances are disposed to release file handles
            foreach (Document doc in docs)
            {
                doc?.Dispose();
            }
        }
    }
}