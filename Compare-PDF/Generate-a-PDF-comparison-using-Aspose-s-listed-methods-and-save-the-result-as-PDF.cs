using System;
using System.IO;
using Aspose.Pdf;

class PdfComparison
{
    static void Main()
    {
        // Input PDF files to compare
        string pdfPath1 = "input1.pdf";
        string pdfPath2 = "input2.pdf";

        // Output PDF file that will contain the comparison
        string outputPath = "comparison.pdf";

        // Validate input files exist
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

        try
        {
            // Load the two source documents
            Document doc1 = new Document(pdfPath1);
            Document doc2 = new Document(pdfPath2);

            // Create an empty document that will hold the comparison
            Document resultDoc = new Document();

            // Append all pages from the first document
            resultDoc.Pages.Add(doc1.Pages);

            // Append all pages from the second document
            resultDoc.Pages.Add(doc2.Pages);

            // Save the combined document as PDF
            // Using the provided document-save rule
            resultDoc.Save(outputPath);

            Console.WriteLine($"Comparison PDF created successfully at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF comparison: {ex.Message}");
        }
    }
}