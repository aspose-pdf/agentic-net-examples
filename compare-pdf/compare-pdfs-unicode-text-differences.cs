using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PdfUnicodeComparer
{
    // Extracts all text from a PDF document, preserving page order.
    static string ExtractText(string pdfPath)
    {
        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"File not found: {pdfPath}");

        using (Document doc = new Document(pdfPath))
        {
            // TextAbsorber extracts text from the whole document.
            TextAbsorber absorber = new TextAbsorber();
            // Use Pure mode to get raw Unicode characters without formatting.
            absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
            doc.Pages.Accept(absorber);
            return absorber.Text;
        }
    }

    // Compares two Unicode strings and reports the first differing character (if any).
    static void CompareTexts(string textA, string textB)
    {
        if (textA == textB)
        {
            Console.WriteLine("Documents are identical (Unicode text matches).");
            return;
        }

        // Find the first index where the texts differ.
        int minLen = Math.Min(textA.Length, textB.Length);
        int diffIndex = -1;
        for (int i = 0; i < minLen; i++)
        {
            if (textA[i] != textB[i])
            {
                diffIndex = i;
                break;
            }
        }

        // If no difference found within the shorter length, the longer document has extra text.
        if (diffIndex == -1)
        {
            Console.WriteLine("Documents differ in length.");
            Console.WriteLine($"Document A length: {textA.Length}");
            Console.WriteLine($"Document B length: {textB.Length}");
            return;
        }

        // Show surrounding context for the difference.
        int contextRadius = 20;
        int start = Math.Max(0, diffIndex - contextRadius);
        int endA = Math.Min(textA.Length, diffIndex + contextRadius);
        int endB = Math.Min(textB.Length, diffIndex + contextRadius);

        string snippetA = textA.Substring(start, endA - start);
        string snippetB = textB.Substring(start, endB - start);

        Console.WriteLine($"Unicode text difference detected at character index {diffIndex}:");
        Console.WriteLine($"Document A char: U+{((int)textA[diffIndex]):X4} ('{textA[diffIndex]}')");
        Console.WriteLine($"Document B char: U+{((int)textB[diffIndex]):X4} ('{textB[diffIndex]}')");
        Console.WriteLine("Context around the difference (Document A):");
        Console.WriteLine(snippetA);
        Console.WriteLine("Context around the difference (Document B):");
        Console.WriteLine(snippetB);
    }

    static void Main()
    {
        // Paths to the PDFs to compare.
        const string pdfPathA = "document_en.pdf";   // e.g., English/Latin encoding
        const string pdfPathB = "document_zh.pdf";   // e.g., Chinese/Unicode encoding

        try
        {
            string textA = ExtractText(pdfPathA);
            string textB = ExtractText(pdfPathB);

            CompareTexts(textA, textB);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}