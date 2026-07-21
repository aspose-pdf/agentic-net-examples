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

        // Load the two PDFs inside using blocks to ensure proper disposal
        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            // Merge the second document into the first one
            // Document.Merge(params Document[]) accepts a variable number of documents
            doc1.Merge(doc2);

            // Save the merged result to the specified output path
            doc1.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}