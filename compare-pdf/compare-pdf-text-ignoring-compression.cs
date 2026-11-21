using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPathA = "documentA.pdf"; // PDF with one compression setting
        const string pdfPathB = "documentB.pdf"; // Same content, different compression

        if (!File.Exists(pdfPathA) || !File.Exists(pdfPathB))
        {
            Console.Error.WriteLine("One or both PDF files are missing.");
            return;
        }

        // Load first PDF and extract its textual content
        string textA;
        using (Document docA = new Document(pdfPathA))
        {
            TextAbsorber absorberA = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            docA.Pages.Accept(absorberA);
            textA = absorberA.Text;
        }

        // Load second PDF and extract its textual content
        string textB;
        using (Document docB = new Document(pdfPathB))
        {
            TextAbsorber absorberB = new TextAbsorber
            {
                ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
            };
            docB.Pages.Accept(absorberB);
            textB = absorberB.Text;
        }

        // Compare the extracted texts
        if (string.Equals(textA, textB, StringComparison.Ordinal))
        {
            Console.WriteLine("The PDFs contain identical text. Compression differences do not affect the textual diff.");
        }
        else
        {
            Console.WriteLine("Textual differences detected between the PDFs.");
            // Simple diff output (show first differing line)
            using (StringReader readerA = new StringReader(textA))
            using (StringReader readerB = new StringReader(textB))
            {
                string lineA, lineB;
                int lineNumber = 1;
                while ((lineA = readerA.ReadLine()) != null && (lineB = readerB.ReadLine()) != null)
                {
                    if (!string.Equals(lineA, lineB, StringComparison.Ordinal))
                    {
                        Console.WriteLine($"Difference at line {lineNumber}:");
                        Console.WriteLine($"PDF A: {lineA}");
                        Console.WriteLine($"PDF B: {lineB}");
                        break;
                    }
                    lineNumber++;
                }
            }
        }
    }
}