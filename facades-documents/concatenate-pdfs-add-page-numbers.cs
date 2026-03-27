using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF files to concatenate
        string[] inputFiles = new string[] { "file1.pdf", "file2.pdf", "file3.pdf" };
        string outputFile = "merged.pdf";

        // Verify that all input files exist
        foreach (string filePath in inputFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Input file not found: {filePath}");
                return;
            }
        }

        // Load source documents
        Document[] sourceDocs = new Document[inputFiles.Length];
        for (int i = 0; i < inputFiles.Length; i++)
        {
            sourceDocs[i] = new Document(inputFiles[i]);
        }

        // Destination document (empty)
        using (Document destDoc = new Document())
        {
            // Concatenate source documents into the destination document
            PdfFileEditor editor = new PdfFileEditor();
            bool concatenated = editor.Concatenate(sourceDocs, destDoc);
            if (!concatenated)
            {
                Console.Error.WriteLine("Failed to concatenate PDF files.");
                return;
            }

            // Add page numbers to each page of the combined document
            for (int pageIndex = 1; pageIndex <= destDoc.Pages.Count; pageIndex++)
            {
                Page page = destDoc.Pages[pageIndex];
                TextFragment pageNumberFragment = new TextFragment(pageIndex.ToString());
                pageNumberFragment.TextState.FontSize = 12;
                pageNumberFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
                // Position near the bottom‑right corner of the page
                pageNumberFragment.Position = new Position(page.PageInfo.Width - 50, 20);
                page.Paragraphs.Add(pageNumberFragment);
            }

            // Save the merged PDF with page numbers
            destDoc.Save(outputFile);
        }

        // Dispose source documents
        foreach (Document srcDoc in sourceDocs)
        {
            srcDoc.Dispose();
        }

        Console.WriteLine($"Merged PDF saved to '{outputFile}'.");
    }
}