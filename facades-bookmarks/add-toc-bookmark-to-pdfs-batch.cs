using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class BatchAddTocBookmark
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = @"C:\PdfInput";
        // Folder where the processed PDFs will be saved
        const string outputFolder = @"C:\PdfOutput";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Determine the output file path (same file name, different folder)
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(inputPath));

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a bookmark editor, bind it to the loaded document and add the TOC bookmark
                using (PdfBookmarkEditor bookmarkEditor = new PdfBookmarkEditor())
                {
                    bookmarkEditor.BindPdf(doc);

                    // Add a standard "Table of Contents" bookmark that points to the first page
                    // CreateBookmarkOfPage is the correct method – AddBookmark does not exist.
                    bookmarkEditor.CreateBookmarkOfPage("Table of Contents", 1);

                    // Save the modified document to the output location
                    bookmarkEditor.Save(outputPath);
                }
            }

            Console.WriteLine($"Processed: {Path.GetFileName(inputPath)} → {outputPath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}
