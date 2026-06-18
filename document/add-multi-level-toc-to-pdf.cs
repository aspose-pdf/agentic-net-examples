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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging so that headings are detected
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            // Use default heading levels (font‑size based mapping)
            AutoTaggingSettings.Default.HeadingLevels = new HeadingLevels();

            // Process paragraphs to create the logical structure with headings
            doc.ProcessParagraphs();

            // Access the tagged‑content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Create a TOC element and attach it to the root of the structure tree
            StructureElement root = taggedContent.RootElement;
            var tocElement = taggedContent.CreateTOCElement();
            root.AppendChild(tocElement);

            // Insert a new page at the beginning to hold the TOC
            doc.Pages.Insert(1);
            Page tocPage = doc.Pages[1];

            // Configure TOC appearance (show page numbers, set title, etc.)
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"),
                IsShowPageNumbers = true,
                CopyToOutlines = true   // optional: copy TOC entries to the PDF outline
            };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑level TOC saved to '{outputPath}'.");
    }
}
