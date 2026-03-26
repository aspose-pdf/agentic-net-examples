using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "replaced.pdf";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("Input files not found.");
            return;
        }

        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            ComparisonOptions options = new ComparisonOptions();
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

            string sourceText = TextPdfComparer.AssemblySourcePageText(diffs);
            Console.WriteLine("Original text from first PDF:");
            Console.WriteLine(sourceText);

            // Replace text on each page of the second document with the source text
            for (int i = 1; i <= doc2.Pages.Count; i++)
            {
                Page page = doc2.Pages[i];
                page.Paragraphs.Clear();
                TextFragment fragment = new TextFragment(sourceText);
                page.Paragraphs.Add(fragment);
            }

            doc2.Save(outputPdf);
        }

        Console.WriteLine($"Replaced PDF saved to '{outputPdf}'.");
    }
}