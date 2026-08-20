using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
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

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging so headings are recognized as structure elements
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Access the tagged‑content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Create a TOC element and attach it to the root of the structure tree
            var tocElement = taggedContent.CreateTOCElement();
            var root = taggedContent.RootElement; // use var to avoid explicit StructureElement reference
            root.AppendChild(tocElement); // one‑argument AppendChild is correct

            // Insert a new page at the beginning of the document to hold the TOC
            Page tocPage = doc.Pages.Insert(1);

            // Configure the TOC page – this makes the TOC visible and copies entries to outlines
            tocPage.TocInfo = new TocInfo
            {
                CopyToOutlines   = true,   // generate clickable outline entries
                IsShowPageNumbers = true,  // show page numbers in the TOC
                Title            = new TextFragment("Table of Contents") // Title must be a TextFragment
            };

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with generated TOC saved to '{outputPath}'.");
    }
}
