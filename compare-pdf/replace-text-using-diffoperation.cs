using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "second_updated.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Compare the first pages of the two documents
            ComparisonOptions compareOptions = new ComparisonOptions();
            List<DiffOperation> diffs = TextPdfComparer.ComparePages(doc1.Pages[1], doc2.Pages[1], compareOptions);

            // Reconstruct the original text from the first PDF using the diff list
            string originalText = TextPdfComparer.AssemblySourcePageText(diffs);

            // Replace the entire text content of the corresponding page in the second PDF
            Page targetPage = doc2.Pages[1];
            targetPage.Paragraphs.Clear();                     // Remove existing content
            TextFragment fragment = new TextFragment(originalText);
            targetPage.Paragraphs.Add(fragment);               // Insert restored text

            // Save the modified second PDF
            doc2.Save(outputPdfPath);
        }

        Console.WriteLine($"Second PDF updated and saved to '{outputPdfPath}'.");
    }
}