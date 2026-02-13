using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDFs
        const string sourcePath = "source.pdf";
        const string destinationPath = "dest.pdf";
        const string outputPath = "merged.pdf";

        // Verify that both input files exist
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        // Load the source and destination documents
        Document sourceDoc = new Document(sourcePath);
        Document destDoc = new Document(destinationPath);

        // Append all pages from the source document to the destination document
        // PageCollection.Add(ICollection<Page>) adds the pages in order
        destDoc.Pages.Add(sourceDoc.Pages);

        // Save the merged document (uses the provided document-save rule)
        destDoc.Save(outputPath);
    }
}