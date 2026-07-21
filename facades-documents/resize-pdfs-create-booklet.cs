using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Desired page dimensions (points). 1 point = 1/72 inch.
    // 1024 x 768 points correspond to roughly 14.22" x 10.67".
    const double TargetWidth = 1024;
    const double TargetHeight = 768;

    static void Main()
    {
        // Input PDF files (adjust paths as needed)
        string[] inputFiles = new string[]
        {
            "input1.pdf",
            "input2.pdf",
            "input3.pdf"
        };

        // Validate existence
        foreach (var file in inputFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Input file not found: {file}");
                return;
            }
        }

        // List to hold paths of resized PDFs
        List<string> resizedFiles = new List<string>();

        // Resize each PDF to the target dimensions
        foreach (var srcPath in inputFiles)
        {
            // Determine all page numbers for the source document
            int[] allPages;
            using (Document srcDoc = new Document(srcPath))
            {
                int pageCount = srcDoc.Pages.Count;
                allPages = new int[pageCount];
                for (int i = 1; i <= pageCount; i++)
                {
                    allPages[i - 1] = i; // 1‑based indexing
                }
            }

            // Create a temporary file for the resized output
            string resizedPath = Path.Combine(Path.GetTempPath(),
                $"{Path.GetFileNameWithoutExtension(srcPath)}_resized.pdf");

            // Perform the resize operation
            PdfFileEditor editor = new PdfFileEditor();
            editor.ResizeContents(srcPath, resizedPath, allPages, TargetWidth, TargetHeight);

            resizedFiles.Add(resizedPath);
        }

        // Concatenate all resized PDFs into a single document
        string concatenatedPath = Path.Combine(Path.GetTempPath(), "concatenated.pdf");
        PdfFileEditor concatEditor = new PdfFileEditor();
        concatEditor.Concatenate(resizedFiles.ToArray(), concatenatedPath);

        // Create a booklet from the concatenated PDF
        string bookletPath = "final_booklet.pdf";
        PdfFileEditor bookletEditor = new PdfFileEditor();
        bookletEditor.MakeBooklet(concatenatedPath, bookletPath);

        // Cleanup temporary files (optional)
        foreach (var tempFile in resizedFiles)
        {
            try { File.Delete(tempFile); } catch { }
        }
        try { File.Delete(concatenatedPath); } catch { }

        Console.WriteLine($"Booklet created: {bookletPath}");
    }
}