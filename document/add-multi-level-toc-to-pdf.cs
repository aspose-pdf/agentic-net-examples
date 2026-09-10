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
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable auto‑tagging globally (uses default heading detection)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Apply auto‑tagging to detect headings and structure
            doc.ProcessParagraphs();

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Create a TOC element and attach it to the root of the structure tree
            TOCElement tocElement = tagged.CreateTOCElement();
            StructureElement root = tagged.RootElement;
            root.AppendChild(tocElement);

            // Insert a new blank page at the beginning to hold the TOC
            Page tocPage = doc.Pages.Insert(1); // Inserts a new empty page at position 1

            // Configure TOC appearance and behavior
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title must be a TextFragment
                IsShowPageNumbers = true,   // show page numbers next to entries
                CopyToOutlines = true,      // copy TOC entries to the PDF outline
                IsCountTocPages = false    // do not count the TOC page itself in page numbers
            };

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑level TOC saved to '{outputPath}'.");
    }
}
