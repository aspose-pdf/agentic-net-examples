using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    // Extracts all text from a PDF document using TextAbsorber.
    static string ExtractText(Document doc)
    {
        // Use Pure formatting to get raw Unicode characters.
        TextAbsorber absorber = new TextAbsorber
        {
            ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
        };
        // Apply absorber to all pages.
        doc.Pages.Accept(absorber);
        return absorber.Text ?? string.Empty;
    }

    // Simple line‑by‑line diff that reports differing lines.
    static void ReportDifferences(string textA, string textB)
    {
        var linesA = textA.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        var linesB = textB.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        int maxLines = Math.Max(linesA.Length, linesB.Length);

        bool anyDiff = false;
        for (int i = 0; i < maxLines; i++)
        {
            string lineA = i < linesA.Length ? linesA[i] : string.Empty;
            string lineB = i < linesB.Length ? linesB[i] : string.Empty;

            if (!lineA.Equals(lineB, StringComparison.Ordinal))
            {
                anyDiff = true;
                Console.WriteLine($"Line {i + 1} differs:");
                Console.WriteLine($"  PDF A: \"{lineA}\"");
                Console.WriteLine($"  PDF B: \"{lineB}\"");
                Console.WriteLine();
            }
        }

        if (!anyDiff)
        {
            Console.WriteLine("No textual differences detected between the two PDFs.");
        }
    }

    static void Main()
    {
        const string pdfPathA = "document_en.pdf";   // PDF with English/Latin text
        const string pdfPathB = "document_zh.pdf";   // PDF with Chinese/Unicode text

        if (!File.Exists(pdfPathA))
        {
            Console.Error.WriteLine($"File not found: {pdfPathA}");
            return;
        }
        if (!File.Exists(pdfPathB))
        {
            Console.Error.WriteLine($"File not found: {pdfPathB}");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal.
            using (Document docA = new Document(pdfPathA))
            using (Document docB = new Document(pdfPathB))
            {
                // Extract Unicode text from each document.
                string textA = ExtractText(docA);
                string textB = ExtractText(docB);

                // Compare the extracted texts and report Unicode differences.
                ReportDifferences(textA, textB);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}