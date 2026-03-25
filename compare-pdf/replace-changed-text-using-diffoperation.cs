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
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "second_fixed.pdf";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        // Load both PDFs
        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            // Compare and obtain the list of differences (DiffOperation objects)
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, new ComparisonOptions());

            // Reconstruct the original text from the first PDF using the diffs
            string originalText = TextPdfComparer.AssemblySourcePageText(diffs);

            // Replace the content of each page in the second PDF with the original text
            foreach (Page page in doc2.Pages)
            {
                page.Paragraphs.Clear();
                TextFragment tf = new TextFragment(originalText);
                page.Paragraphs.Add(tf);
            }

            // Save the modified second PDF
            doc2.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}