using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;               // TextFragment, FontRepository
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string xmlPath   = "headings.xml";   // XML with <heading level=\"1\">Title</heading>
        const string inputPdf  = "input.pdf";      // source PDF (may be empty)
        const string outputPdf = "output_with_toc.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Load XML and extract headings preserving order
        var headings = XDocument.Load(xmlPath)
                                .Descendants("heading");

        // Load (or create) the PDF document
        using (Document doc = File.Exists(inputPdf) ? new Document(inputPdf) : new Document())
        {
            // Ensure the document has at least one page for the TOC
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Insert a new page at the beginning for the TOC
            Page tocPage = doc.Pages.Insert(1);
            tocPage.Paragraphs.Add(new TextFragment("Table of Contents")
            {
                TextState = { FontSize = 20, Font = FontRepository.FindFont("Helvetica-Bold") },
                HorizontalAlignment = HorizontalAlignment.Center
            });

            // Create TOC element in the logical structure
            ITaggedContent tagged = doc.TaggedContent;
            StructureElement root = tagged.RootElement;

            // Create the TOC container
            var tocElement = tagged.CreateTOCElement();
            tocElement.Title = "Table of Contents"; // visible title for the TOC element
            root.AppendChild(tocElement);

            // For each heading, create a TOCI entry and attach it to the TOC
            foreach (var h in headings)
            {
                string title = h.Value.Trim();
                if (string.IsNullOrEmpty(title))
                    continue;

                // Create a TOCI element
                var toci = tagged.CreateTOCIElement();

                // Set the visible text of the TOCI entry
                toci.ActualText = title; // use ActualText instead of non‑existent SetText()

                // Optionally, you could set a reference to a page number here.
                // For simplicity, we leave it as plain text.

                tocElement.AppendChild(toci);
            }

            // Configure TOC page info (show page numbers, copy to outlines, etc.)
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title expects a TextFragment
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with TOC saved to '{outputPdf}'.");
    }
}
