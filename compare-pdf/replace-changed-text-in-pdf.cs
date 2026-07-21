using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;   // TextPdfComparer, DiffOperation, ComparisonOptions
using Aspose.Pdf.Text;         // TextFragment

class ReplaceChangedText
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";   // PDF with original text
        const string secondPdfPath = "second.pdf";  // PDF with changed text
        const string outputPdfPath = "second_fixed.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Compare the first pages (adjust index if needed)
            ComparisonOptions options = new ComparisonOptions();
            List<DiffOperation> diffs = TextPdfComparer.ComparePages(doc1.Pages[1], doc2.Pages[1], options);

            // Reconstruct the original text from the first PDF
            string originalText = TextPdfComparer.AssemblySourcePageText(diffs);

            // Replace the entire content of the second PDF's page with the original text
            Page targetPage = doc2.Pages[1];
            targetPage.Paragraphs.Clear();                     // Remove existing elements
            TextFragment fragment = new TextFragment(originalText);
            targetPage.Paragraphs.Add(fragment);               // Add restored text

            // Save the modified second PDF
            doc2.Save(outputPdfPath);
        }

        Console.WriteLine($"Replaced changed text and saved to '{outputPdfPath}'.");
    }
}