using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files to be bundled into the portfolio
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        const string outputPath = "portfolio.pdf";

        // Verify that all source files exist
        foreach (var file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"Source file not found: {file}");
                return;
            }
        }

        // Create an empty PDF document that will act as the portfolio container
        Document portfolioDoc = new Document();
        // A portfolio must contain at least one page; add a blank page
        portfolioDoc.Pages.Add();

        // Add each source PDF as an embedded file (portfolio entry)
        foreach (var file in pdfFiles)
        {
            // Create a file specification for the embedded file using the overload that accepts a file path
            var fileSpec = new FileSpecification(file, Path.GetFileName(file));
            // Populate the file contents via a stream (required for embedding)
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(file));
            // Add the specification to the document's EmbeddedFiles collection
            portfolioDoc.EmbeddedFiles.Add(fileSpec);
        }

        // Save the resulting PDF portfolio
        portfolioDoc.Save(outputPath);

        Console.WriteLine($"PDF portfolio created at '{outputPath}'.");
    }
}
