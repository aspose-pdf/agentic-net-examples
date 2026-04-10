using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API (Document, PageInfo, etc.)
using Aspose.Pdf.Facades;            // Facade API (PdfContentEditor)

class BatchStampReposition
{
    static void Main()
    {
        // Input PDF files – adjust the list as needed
        string[] inputFiles = new string[]
        {
            "file1.pdf",
            "file2.pdf",
            "file3.pdf"
        };

        // Directory where the processed PDFs will be saved
        string outputDir = "Repositioned";
        Directory.CreateDirectory(outputDir);

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            // Build output file name (e.g., file1_repositioned.pdf)
            string outputPath = Path.Combine(
                outputDir,
                Path.GetFileNameWithoutExtension(inputPath) + "_repositioned.pdf");

            // Load the document to obtain page dimensions (required for positioning)
            using (Document doc = new Document(inputPath))
            {
                // Facade editor for stamp manipulation
                PdfContentEditor editor = new PdfContentEditor();
                editor.BindPdf(inputPath);   // Initialize with the source PDF

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    // Retrieve existing stamps on the current page
                    StampInfo[] stamps = editor.GetStamps(pageNum);
                    if (stamps == null || stamps.Length == 0)
                        continue;   // No stamps on this page

                    // Page size (points; 1 inch = 72 points)
                    double pageWidth  = doc.Pages[pageNum].PageInfo.Width;
                    double pageHeight = doc.Pages[pageNum].PageInfo.Height;

                    // Desired new position: top‑center, 20 points below the top edge
                    double newX = pageWidth / 2.0;   // center horizontally
                    double newY = pageHeight - 20.0; // 20 points from top

                    // Move each stamp on the page
                    // StampInfo array order corresponds to stamp index (1‑based for MoveStamp)
                    for (int i = 0; i < stamps.Length; i++)
                    {
                        int stampIndex = i + 1; // MoveStamp expects 1‑based index
                        editor.MoveStamp(pageNum, stampIndex, newX, newY);
                    }
                }

                // Save the modified PDF
                editor.Save(outputPath);
                editor.Close();   // Release resources held by the facade
            }

            Console.WriteLine($"Processed '{inputPath}' → '{outputPath}'");
        }
    }
}