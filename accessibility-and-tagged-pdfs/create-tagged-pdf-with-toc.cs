using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // Needed for TextFragment

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TaggedDocumentWithTOC.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title (metadata for accessibility)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Document with Table of Contents");

            // -------------------------------------------------
            // 1. Create a Table of Contents (TOC) page
            // -------------------------------------------------
            // Add a new page that will hold the TOC
            Page tocPage = doc.Pages.Add();

            // Configure TOC page info (makes this page a TOC container)
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title expects a TextFragment
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };

            // Create the TOC structure element and attach it to the root
            var tocElement = tagged.CreateTOCElement();
            // Optionally set a visible title for the TOC element
            tocElement.Title = "Table of Contents";
            tagged.RootElement.AppendChild(tocElement);

            // -------------------------------------------------
            // 2. Create sample content pages with headings
            // -------------------------------------------------
            // Helper to add a heading and a paragraph on a new page
            void AddSection(string headingText, string paragraphText)
            {
                // Add a new content page
                Page contentPage = doc.Pages.Add();

                // Create a Header element (level 1) for the section title
                HeaderElement header = tagged.CreateHeaderElement(1);
                header.SetText(headingText);
                // Attach the header to the root of the logical structure (or to a page element as needed)
                tagged.RootElement.AppendChild(header);

                // Create a Paragraph element for the body text
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText(paragraphText);
                tagged.RootElement.AppendChild(paragraph);

                // -------------------------------------------------
                // 3. Add a TOC entry (TOCI) that points to this section
                // -------------------------------------------------
                // Create a TOCI (Table of Contents Item) element
                var tocItem = tagged.CreateTOCIElement();

                // Build a simple label with the heading and page number
                // Note: In a real scenario you would calculate the page number dynamically.
                // Here we use the current page index (1‑based) for demonstration.
                int pageNumber = doc.Pages.Count; // current page is the last added
                tocItem.ActualText = $"{headingText} .................................. {pageNumber}";

                // Append the TOCI entry to the TOC element
                tocElement.AppendChild(tocItem);
            }

            // Add a few sections to demonstrate the TOC
            AddSection("Chapter 1: Introduction", "This is the introductory chapter of the document.");
            AddSection("Chapter 2: Getting Started", "This chapter explains how to get started with the product.");
            AddSection("Chapter 3: Advanced Topics", "This chapter covers advanced usage scenarios.");

            // -------------------------------------------------
            // 4. Save the tagged PDF document (guarded for non‑Windows platforms)
            // -------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Tagged PDF with TOC saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Tagged PDF with TOC saved to '{outputPath}'. (saved on non‑Windows platform)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
