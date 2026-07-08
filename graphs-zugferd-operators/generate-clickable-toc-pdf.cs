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
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging so that headings are detected and a logical structure is built
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Process paragraphs to create the structure tree (headings become HeaderElements)
            doc.ProcessParagraphs();

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Create a TOC element and set its title
            TOCElement tocElement = tagged.CreateTOCElement();
            tocElement.Title = "Table of Contents";

            // Append the TOC element to the root of the structure tree
            StructureElement root = tagged.RootElement;               // no cast required
            root.AppendChild(tocElement);                            // AppendChild with one argument

            // Insert a new page at the beginning of the document to hold the TOC
            Page tocPage = doc.Pages.Insert(1);

            // Configure the TOC page – copy entries to outlines makes them clickable
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title expects a TextFragment
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with generated TOC saved to '{outputPath}'.");
    }
}
