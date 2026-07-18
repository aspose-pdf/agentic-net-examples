using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string firstPdf  = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "concatenated.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // ------------------------------------------------------------
        // Concatenate the two PDFs using PdfFileEditor (Facades API)
        // ------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        bool concatSuccess = editor.Concatenate(firstPdf, secondPdf, outputPdf);
        if (!concatSuccess)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        // ------------------------------------------------------------
        // Validate that page order is preserved after concatenation
        // ------------------------------------------------------------
        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        using (Document resultDoc = new Document(outputPdf))
        {
            int count1 = doc1.Pages.Count;          // pages in first PDF
            int count2 = doc2.Pages.Count;          // pages in second PDF
            int total  = resultDoc.Pages.Count;     // pages in concatenated PDF

            // Basic count check
            if (total != count1 + count2)
            {
                Console.Error.WriteLine($"Page count mismatch. Expected {count1 + count2}, got {total}.");
                return;
            }

            // Helper to extract text from a specific page
            string ExtractPageText(Page page)
            {
                TextAbsorber absorber = new TextAbsorber();
                page.Accept(absorber);
                return absorber.Text ?? string.Empty;
            }

            // Compare pages from the first source PDF
            for (int i = 1; i <= count1; i++)
            {
                string srcText   = ExtractPageText(doc1.Pages[i]);
                string resultText = ExtractPageText(resultDoc.Pages[i]);

                if (!srcText.Equals(resultText, StringComparison.Ordinal))
                {
                    Console.Error.WriteLine($"Mismatch detected on page {i} (first PDF).");
                    return;
                }
            }

            // Compare pages from the second source PDF
            for (int i = 1; i <= count2; i++)
            {
                int resultPageIndex = count1 + i; // offset in the concatenated document
                string srcText   = ExtractPageText(doc2.Pages[i]);
                string resultText = ExtractPageText(resultDoc.Pages[resultPageIndex]);

                if (!srcText.Equals(resultText, StringComparison.Ordinal))
                {
                    Console.Error.WriteLine($"Mismatch detected on page {resultPageIndex} (second PDF).");
                    return;
                }
            }

            Console.WriteLine("Concatenation validated: page order preserved.");
        }
    }
}