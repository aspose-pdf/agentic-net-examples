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
            // Enable auto‑tagging so that headings are detected and structured
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            // (Optional) configure heading levels if needed
            // AutoTaggingSettings.Default.HeadingLevels = new HeadingLevels();

            // Process the document to create the logical structure (headings, etc.)
            doc.ProcessParagraphs();

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Create a TOC element and set its title
            TOCElement tocElement = tagged.CreateTOCElement();
            tocElement.Title = "Table of Contents"; // TOCElement.Title expects a string

            // Append the TOC element to the root of the structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(tocElement);

            // Insert a new page at the beginning of the document to hold the TOC
            Page tocPage = doc.Pages.Insert(1);

            // Configure the TOC page information (show page numbers, copy to outlines, etc.)
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title must be a TextFragment
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑level TOC saved to '{outputPath}'.");
    }
}
