using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "toc_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ----- Heading level 1 -----
            Heading heading1 = new Heading(1);
            heading1.Text = "Chapter 1: Introduction";
            heading1.IsAutoSequence = true;
            // Example of setting a numbering style (property may vary by version)
            // heading1.NumberingStyle = NumberingStyle.Decimal;
            page.Paragraphs.Add(heading1);

            // ----- Heading level 2 (style A) -----
            Heading heading2A = new Heading(2);
            heading2A.Text = "Section 1.1: Overview";
            heading2A.IsAutoSequence = true;
            // heading2A.NumberingStyle = NumberingStyle.LowerRoman;
            page.Paragraphs.Add(heading2A);

            // ----- Heading level 2 (style B) -----
            Heading heading2B = new Heading(2);
            heading2B.Text = "Section 1.2: Details";
            heading2B.IsAutoSequence = true;
            // heading2B.NumberingStyle = NumberingStyle.UpperRoman;
            page.Paragraphs.Add(heading2B);

            // ----- Heading level 3 -----
            Heading heading3 = new Heading(3);
            heading3.Text = "Subsection 1.2.1: Deep Dive";
            heading3.IsAutoSequence = true;
            // heading3.NumberingStyle = NumberingStyle.Alpha;
            page.Paragraphs.Add(heading3);

            // ----- Build a structured Table of Contents (TOC) -----
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with TOC");

            // Root element of the tagged structure
            StructureElement root = tagged.RootElement;

            // Create a TOC element and attach it to the root
            TOCElement toc = tagged.CreateTOCElement();
            toc.AlternativeText = "Table of Contents";
            root.AppendChild(toc);

            // Save the PDF (no extra SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with headings and TOC saved to '{outputPath}'.");
    }
}