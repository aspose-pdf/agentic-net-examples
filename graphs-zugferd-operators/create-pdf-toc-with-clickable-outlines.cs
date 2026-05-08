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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging so headings are recognized and outline entries are created
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Create a TOC element and attach it to the root of the structure tree
            TOCElement tocElement = tagged.CreateTOCElement();
            StructureElement root = tagged.RootElement;
            root.AppendChild(tocElement);

            // Insert a new page at the beginning of the document to hold the TOC
            Page tocPage = doc.Pages.Insert(1);

            // Configure the TOC page – title, page numbers, and copy entries to outlines (bookmarks)
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title must be a TextFragment
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with generated TOC saved to '{outputPath}'.");
    }
}
