using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, CgmLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input CGM files to be merged – adjust the paths as needed.
        string[] cgmFiles = { "first.cgm", "second.cgm", "third.cgm" };
        const string outputPdf = "merged_output.pdf";

        // Verify that all input files exist before proceeding.
        foreach (string path in cgmFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"Input file not found: {path}");
                return;
            }
        }

        try
        {
            // Load the first CGM file – this will become the target document.
            using (Document target = new Document(cgmFiles[0], new CgmLoadOptions()))
            {
                // Iterate over the remaining CGM files, load each, and append its pages to the target.
                for (int i = 1; i < cgmFiles.Length; i++)
                {
                    using (Document source = new Document(cgmFiles[i], new CgmLoadOptions()))
                    {
                        // Append all pages from the source document to the target document.
                        target.Pages.Add(source.Pages);
                    } // source Document disposed here.
                }

                // Save the merged result as a PDF file.
                target.Save(outputPdf);
            } // target Document disposed here.

            Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge: {ex.Message}");
        }
    }
}