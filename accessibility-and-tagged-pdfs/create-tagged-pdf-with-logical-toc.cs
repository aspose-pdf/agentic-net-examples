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
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (write‑only setters)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Document with TOC");

            // -----------------------------------------------------------------
            // 1. Create a logical Table of Contents (TOC) structure element
            // -----------------------------------------------------------------
            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // TOC element (container for TOC items)
            TOCElement toc = tagged.CreateTOCElement();
            // Optional: set a visible title for the TOC element itself
            toc.Title = "Table of Contents";
            root.AppendChild(toc); // attach TOC to the root

            // -----------------------------------------------------------------
            // 2. Add TOC entries (TOCI elements) – one per heading
            // -----------------------------------------------------------------
            // Example headings – in a real scenario these could be derived from the document
            string[] headings = { "Introduction", "Chapter 1 – Getting Started", "Chapter 2 – Advanced Topics", "Conclusion" };
            int[] pageNumbers = { 2, 3, 5, 7 }; // illustrative target pages

            for (int i = 0; i < headings.Length; i++)
            {
                // Create a TOCI (Table of Contents Item) element
                TOCIElement entry = tagged.CreateTOCIElement();

                // Set the visible label (the heading text) using ActualText property
                entry.ActualText = headings[i];

                // Create a Reference element and set its text (page number) via ActualText
                ReferenceElement reference = tagged.CreateReferenceElement();
                reference.ActualText = pageNumbers[i].ToString();

                // Append the reference to the TOCI entry
                entry.AppendChild(reference);

                // Append the TOCI entry to the TOC container
                toc.AppendChild(entry);
            }

            // -----------------------------------------------------------------
            // 3. Add a physical page that will display the TOC
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // Configure the page's TOC info (controls visual rendering of the TOC)
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // TextFragment required
                IsShowPageNumbers = true,
                CopyToOutlines = false // do not duplicate in outlines
            };

            // -----------------------------------------------------------------
            // 4. Save the document
            // -----------------------------------------------------------------
            doc.Save("TaggedDocumentWithTOC.pdf");
        }

        Console.WriteLine("PDF with logical TOC created successfully.");
    }
}
