using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Add Heading objects with different levels
            // -------------------------------------------------
            // Page 1 – Level 1 heading
            Aspose.Pdf.Page page1 = doc.Pages[1];
            Aspose.Pdf.Heading h1 = new Aspose.Pdf.Heading(1)
            {
                Text = "Chapter 1 – Introduction",
                IsAutoSequence = true,          // enable automatic numbering
                IsInList = true                 // include in TOC list
            };
            // Styling (optional)
            h1.TextState.Font = FontRepository.FindFont("Helvetica");
            h1.TextState.FontSize = 20;
            h1.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            page1.Paragraphs.Add(h1);

            // Page 2 – Level 2 heading
            Aspose.Pdf.Page page2 = doc.Pages[2];
            Aspose.Pdf.Heading h2 = new Aspose.Pdf.Heading(2)
            {
                Text = "1.1 Overview",
                IsAutoSequence = true,
                IsInList = true
            };
            h2.TextState.Font = FontRepository.FindFont("Helvetica");
            h2.TextState.FontSize = 16;
            h2.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGreen;
            page2.Paragraphs.Add(h2);

            // Page 3 – Level 3 heading
            Aspose.Pdf.Page page3 = doc.Pages[3];
            Aspose.Pdf.Heading h3 = new Aspose.Pdf.Heading(3)
            {
                Text = "1.1.1 Details",
                IsAutoSequence = true,
                IsInList = true
            };
            h3.TextState.Font = FontRepository.FindFont("Helvetica");
            h3.TextState.FontSize = 14;
            h3.TextState.ForegroundColor = Aspose.Pdf.Color.Brown;
            page3.Paragraphs.Add(h3);

            // -------------------------------------------------
            // 2. Build a structured Table of Contents (TOC)
            // -------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;

            // Ensure the document is tagged (auto‑tagging can be enabled if needed)
            Aspose.Pdf.AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a TOC element and attach it to the root
            TOCElement toc = tagged.CreateTOCElement();
            root.AppendChild(toc);

            // Create TOC entries (TOCI) that reference the headings.
            // The Heading objects already have IsInList = true, which makes them
            // appear in the TOC when the TOC element is present.
            // No additional code is required to link them; the logical structure
            // engine will resolve the references automatically.

            // -------------------------------------------------
            // 3. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with headings and TOC saved to '{outputPath}'.");
    }
}