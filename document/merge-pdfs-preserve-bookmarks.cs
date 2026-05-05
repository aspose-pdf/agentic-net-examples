using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents and merge them.
        // Document.Merge copies pages and retains outlines/bookmarks.
        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            doc1.Merge(doc2);          // Append doc2 to doc1, preserving bookmarks
            doc1.Save(outputPdf);      // Save the combined document
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}