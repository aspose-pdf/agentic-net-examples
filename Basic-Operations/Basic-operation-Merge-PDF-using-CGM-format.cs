using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM files to be merged (order matters)
        string[] cgmFiles = { "input1.cgm", "input2.cgm", "input3.cgm" };
        const string outputPdf = "merged_output.pdf";

        // Verify that all input files exist
        foreach (string path in cgmFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"File not found: {path}");
                return;
            }
        }

        // Load the first CGM as the target document
        using (Document target = new Document(cgmFiles[0], new CgmLoadOptions()))
        {
            // Load each subsequent CGM and append its pages to the target
            for (int i = 1; i < cgmFiles.Length; i++)
            {
                using (Document source = new Document(cgmFiles[i], new CgmLoadOptions()))
                {
                    target.Pages.Add(source.Pages);
                }
            }

            // Save the merged result as a PDF
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}