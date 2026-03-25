using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "toc_document.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Logical TOC");

            // -----------------------------------------------------------------
            // 1. Create a TOC page and associate TocInfo
            // -----------------------------------------------------------------
            Page tocPage = doc.Pages.Add();
            tocPage.TocInfo = new TocInfo
            {
                Title = new TextFragment("Table of Contents"), // Title expects a TextFragment
                IsShowPageNumbers = true,
                CopyToOutlines = true
            };

            // Create the TOC structure element and add it to the root
            TOCElement tocElement = tagged.CreateTOCElement();
            tocElement.Title = "Table of Contents"; // Use the Title property, not SetText
            tagged.RootElement.AppendChild(tocElement);

            // -----------------------------------------------------------------
            // 2. Add some heading pages and corresponding TOCI entries
            // -----------------------------------------------------------------
            for (int i = 1; i <= 3; i++)
            {
                // Add a new page for the heading
                Page headingPage = doc.Pages.Add();

                // Add visible text to the page so the PDF is not empty
                string headingText = $"Chapter {i}";
                headingPage.Paragraphs.Add(new TextFragment(headingText));

                // Create a heading (HeaderElement level 1)
                HeaderElement heading = tagged.CreateHeaderElement(1);
                heading.Title = headingText; // Set the heading title
                // Append heading to the document root (or a section if desired)
                tagged.RootElement.AppendChild(heading);

                // Create a TOCI entry for this heading
                TOCIElement tocItem = tagged.CreateTOCIElement();
                tocItem.ActualText = headingText; // Use ActualText, not SetText
                tocElement.AppendChild(tocItem);
            }

            // -----------------------------------------------------------------
            // 3. Save the PDF – guard against missing GDI+ on non‑Windows platforms
            // -----------------------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with logical TOC saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF with logical TOC saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
