using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string firstPdf  = "file1.pdf";
        const string secondPdf = "file2.pdf";
        const string outputPdf = "concatenated.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"Input file not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"Input file not found: {secondPdf}");
            return;
        }

        // Concatenate the two PDFs using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool concatResult = editor.Concatenate(firstPdf, secondPdf, outputPdf);
        if (!concatResult)
        {
            Console.Error.WriteLine("Concatenation failed.");
            return;
        }

        // Load the original and concatenated documents
        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        using (Document concatenated = new Document(outputPdf))
        {
            // Extract page texts from each document
            List<string> texts1 = ExtractPageTexts(doc1);
            List<string> texts2 = ExtractPageTexts(doc2);
            List<string> textsConcat = ExtractPageTexts(concatenated);

            // Validate total page count
            int expectedCount = texts1.Count + texts2.Count;
            if (textsConcat.Count != expectedCount)
            {
                Console.Error.WriteLine($"Page count mismatch after concatenation. Expected {expectedCount}, got {textsConcat.Count}.");
                return;
            }

            // Validate order: first part should match first PDF, second part should match second PDF
            bool orderPreserved = true;

            for (int i = 0; i < texts1.Count; i++)
            {
                if (!string.Equals(texts1[i], textsConcat[i], StringComparison.Ordinal))
                {
                    orderPreserved = false;
                    Console.Error.WriteLine($"Page {i + 1} differs from first source PDF.");
                    break;
                }
            }

            if (orderPreserved)
            {
                for (int i = 0; i < texts2.Count; i++)
                {
                    int concatIndex = texts1.Count + i;
                    if (!string.Equals(texts2[i], textsConcat[concatIndex], StringComparison.Ordinal))
                    {
                        orderPreserved = false;
                        Console.Error.WriteLine($"Page {concatIndex + 1} differs from second source PDF.");
                        break;
                    }
                }
            }

            if (orderPreserved)
                Console.WriteLine("Concatenation preserved original page order.");
            else
                Console.Error.WriteLine("Concatenation did NOT preserve original page order.");
        }
    }

    // Helper: extracts the textual content of each page in a document
    private static List<string> ExtractPageTexts(Document doc)
    {
        var pageTexts = new List<string>();
        // Aspose.Pdf uses 1‑based page indexing
        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            var page = doc.Pages[i];
            TextAbsorber absorber = new TextAbsorber();
            page.Accept(absorber);
            pageTexts.Add(absorber.Text ?? string.Empty);
        }
        return pageTexts;
    }
}