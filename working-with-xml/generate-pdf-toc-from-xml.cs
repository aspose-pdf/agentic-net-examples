using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // needed for TextFragment

class Program
{
    static void Main()
    {
        const string xmlPath   = "headings.xml";   // XML containing heading hierarchy
        const string inputPdf  = "input.pdf";      // Source PDF
        const string outputPdf = "output_with_toc.pdf";

        if (!File.Exists(xmlPath) || !File.Exists(inputPdf))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Load heading hierarchy from XML.
        // Expected format: <headings><heading level="1">Title</heading>...</headings>
        XDocument xdoc   = XDocument.Load(xmlPath);
        var       headings = xdoc.Root.Descendants("heading");

        // Open the PDF inside a using block (lifecycle rule).
        using (Document doc = new Document(inputPdf))
        {
            // Work with tagged content.
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPdf));

            // Insert a new page at the beginning to hold the TOC.
            Page tocPage = doc.Pages.Insert(1);
            tocPage.TocInfo = new TocInfo
            {
                Title               = new TextFragment("Table of Contents"), // Fixed: TextFragment required
                IsShowPageNumbers   = true,
                CopyToOutlines      = false
            };

            // Create the top‑level TOC structure element and attach it to the root.
            StructureElement root      = tagged.RootElement;
            var             tocElement = tagged.CreateTOCElement();
            root.AppendChild(tocElement);

            // For each heading create a TOCI entry.
            foreach (var h in headings)
            {
                string headingText = h.Value.Trim();

                // Create a TOCI element and set its visible text.
                var toci = tagged.CreateTOCIElement();
                toci.ActualText = headingText; // Fixed: use ActualText instead of SetText

                // Attach the TOCI entry to the TOC.
                tocElement.AppendChild(toci);
            }

            // Save the modified PDF (lifecycle rule).
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with generated TOC saved to '{outputPdf}'.");
    }
}
