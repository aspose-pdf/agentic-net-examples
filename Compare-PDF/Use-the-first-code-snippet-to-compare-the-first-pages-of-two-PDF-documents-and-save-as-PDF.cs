using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";

        // Output PDF that will contain the compared pages
        const string outputPath = "comparison.pdf";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load the two source documents
        Document doc1 = new Document(pdfPath1);
        Document doc2 = new Document(pdfPath2);

        // Ensure each document has at least one page
        if (doc1.Pages.Count < 1 || doc2.Pages.Count < 1)
        {
            Console.Error.WriteLine("One of the PDFs does not contain any pages.");
            return;
        }

        // Create a new document that will hold the comparison result
        Document result = new Document();

        // Add the first page of the first document
        result.Pages.Add(doc1.Pages[1]);

        // Add the first page of the second document
        result.Pages.Add(doc2.Pages[1]);

        // Save the resulting PDF
        result.Save(outputPath);
    }
}