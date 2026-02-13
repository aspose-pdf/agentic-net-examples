using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfComparer
{
    static void Main(string[] args)
    {
        // Input PDF file paths
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";
        // Output PDF file path
        const string resultPath = "comparisonResult.pdf";

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

        try
        {
            // Load the two documents to be compared
            Document doc1 = new Document(pdfPath1);
            Document doc2 = new Document(pdfPath2);

            // Create a new empty document that will hold the comparison result
            Document resultDoc = new Document();

            // Add a page to the result document
            Page resultPage = resultDoc.Pages.Add();

            // Build a textual summary of the comparison
            string summary = $"Comparison of PDFs\r\n" +
                             $"-------------------\r\n" +
                             $"Document 1: \"{Path.GetFileName(pdfPath1)}\" – {doc1.Pages.Count} page(s)\r\n" +
                             $"Document 2: \"{Path.GetFileName(pdfPath2)}\" – {doc2.Pages.Count} page(s)\r\n";

            // If the page counts differ, note the difference
            if (doc1.Pages.Count != doc2.Pages.Count)
            {
                summary += "\r\nResult: The documents have a different number of pages.";
            }
            else
            {
                summary += "\r\nResult: Both documents contain the same number of pages.";
            }

            // Add the summary as a text fragment to the result page
            TextFragment tf = new TextFragment(summary);
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            resultPage.Paragraphs.Add(tf);

            // Save the result document using the prescribed rule
            resultDoc.Save(resultPath);
            Console.WriteLine($"Comparison PDF saved to: {resultPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}