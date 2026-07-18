using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Folder containing PDFs and their matching XFDF files
        const string inputFolder = "InputFiles";
        // Path for the merged output PDF
        const string outputPath = "merged_output.pdf";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Collect all PDF files in the folder (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files found to process.");
            return;
        }

        // Create the target document that will hold all merged pages
        using (Document target = new Document())
        {
            // Iterate over each source PDF
            foreach (string pdfPath in pdfFiles)
            {
                // Load the source PDF
                using (Document src = new Document(pdfPath))
                {
                    // Look for a matching XFDF file (same name, .xfdf extension)
                    string xfdfPath = Path.ChangeExtension(pdfPath, ".xfdf");
                    if (File.Exists(xfdfPath))
                    {
                        // Import annotations from the XFDF into the source PDF
                        src.ImportAnnotationsFromXfdf(xfdfPath);
                    }

                    // Append all pages from the source PDF to the target document
                    target.Pages.Add(src.Pages);
                } // src disposed here – pages have already been copied into target
            }

            // Save the merged document with all annotations preserved
            target.Save(outputPath);
        } // target disposed here

        Console.WriteLine($"Merged PDF with annotations saved to '{outputPath}'.");
    }
}