using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Insert a new page at the beginning to hold the TOC
            Page tocPage = doc.Pages.Insert(1);

            // Configure TOC page information
            TocInfo tocInfo = new TocInfo
            {
                // Title expects a TextFragment, not a plain string
                Title = new TextFragment("Table of Contents"),
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };
            tocPage.TocInfo = tocInfo;

            // Create the TOC structure element
            TOCElement tocElement = tagged.CreateTOCElement();

            // Append the TOC element to the root of the logical structure
            StructureElement root = tagged.RootElement;
            root.AppendChild(tocElement);

            // ----- Example TOC entries (TOCI) -----
            // First entry
            TOCIElement entry1 = tagged.CreateTOCIElement();
            // TOCIElement.Title is a string, not a TextFragment
            entry1.Title = "Chapter 1 – Introduction";
            // AlternativeText is also a string; we store the target page number as a placeholder
            entry1.AlternativeText = "2";
            tocElement.AppendChild(entry1);

            // Second entry
            TOCIElement entry2 = tagged.CreateTOCIElement();
            entry2.Title = "Chapter 2 – Details";
            entry2.AlternativeText = "5";
            tocElement.AppendChild(entry2);
            // -------------------------------------

            // Save the modified PDF (output format is PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table of contents saved to '{outputPath}'.");
    }
}
