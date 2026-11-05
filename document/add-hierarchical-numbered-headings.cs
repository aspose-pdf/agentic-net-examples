using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "numbered_headings.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (or create a new one if you prefer)
        using (Document doc = new Document(inputPath))
        {
            // For simplicity we add the headings to the first page.
            // In a real scenario you would decide where each heading belongs.
            Page page = doc.Pages[1];

            // ----- First heading (H1) -----
            Heading heading1 = new Heading(1)
            {
                Text = "Chapter 1: Introduction",
                // Decimal (Arabic) numbering
                Style = NumberingStyle.NumeralsArabic,
                // Start numbering at 1 and let Aspose handle the sequence
                StartNumber = 1,
                IsAutoSequence = true,
                // Mark as part of a list so hierarchical numbering is shown
                IsInList = true
            };
            page.Paragraphs.Add(heading1);

            // ----- Sub‑heading (H2) under the first heading -----
            Heading heading2 = new Heading(2)
            {
                Text = "Section 1.1: Overview",
                Style = NumberingStyle.NumeralsArabic,
                StartNumber = 1,
                IsAutoSequence = true,
                IsInList = true
            };
            page.Paragraphs.Add(heading2);

            // ----- Second top‑level heading (H1) continuing the numbering -----
            Heading heading3 = new Heading(1)
            {
                Text = "Chapter 2: Details",
                Style = NumberingStyle.NumeralsArabic,
                // Continue numbering from the previous H1 (which started at 1)
                StartNumber = 2,
                IsAutoSequence = true,
                IsInList = true
            };
            page.Paragraphs.Add(heading3);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with numbered headings saved to '{outputPath}'.");
    }
}
