using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the individual PDF files generated from DataTables
        string[] pdfFiles = {
            "Report_Part1.pdf",
            "Report_Part2.pdf",
            "Report_Part3.pdf"
            // add more file names as needed
        };

        // Validate input files
        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // The first PDF will be the base document; subsequent PDFs are merged into it
        string outputPath = "ConsolidatedReport.pdf";

        // Use the documented merging pattern with nested using blocks
        using (Document target = new Document(pdfFiles[0]))
        {
            // Iterate over the remaining PDFs and append their pages
            for (int i = 1; i < pdfFiles.Length; i++)
            {
                using (Document source = new Document(pdfFiles[i]))
                {
                    target.Pages.Add(source.Pages);
                }
            }

            // Save the merged document; target must remain alive until Save completes
            target.Save(outputPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}