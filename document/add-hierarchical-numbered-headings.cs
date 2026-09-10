using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // First‑level heading (decimal numbering is the default)
            Heading heading1 = new Heading(1)
            {
                Text = "Chapter 1",
                IsAutoSequence = true,
                Level = 1
                // NumberingStyle property does not exist; default is Arabic (decimal)
            };
            doc.Pages[1].Paragraphs.Add(heading1);

            // Second‑level heading – inherits hierarchical numbering
            Heading heading2 = new Heading(2)
            {
                Text = "Section 1.1",
                IsAutoSequence = true,
                Level = 2
            };
            doc.Pages[1].Paragraphs.Add(heading2);

            // Another first‑level heading to demonstrate continued numbering
            Heading heading3 = new Heading(1)
            {
                Text = "Chapter 2",
                IsAutoSequence = true,
                Level = 1
            };
            doc.Pages[1].Paragraphs.Add(heading3);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hierarchical headings saved to '{outputPath}'.");
    }
}
