using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "TableOfContents.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -----------------------------------------------------------------
            // Create section pages with headings
            // -----------------------------------------------------------------
            // Store page numbers for each section to use in the TOC links
            int[] sectionPages = new int[3];

            for (int i = 0; i < 3; i++)
            {
                // Add a new page for the section
                Page secPage = doc.Pages.Add();

                // Record the page number (1‑based indexing)
                sectionPages[i] = secPage.Number;

                // Create a heading text fragment
                TextFragment heading = new TextFragment($"Section {i + 1}");
                heading.TextState.Font = FontRepository.FindFont("Helvetica");
                heading.TextState.FontSize = 24;
                heading.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
                heading.Position = new Position(50, 750); // place near top

                // Add the heading to the page
                secPage.Paragraphs.Add(heading);

                // Add some dummy body text
                TextFragment body = new TextFragment("Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                                                      "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
                body.TextState.Font = FontRepository.FindFont("Helvetica");
                body.TextState.FontSize = 12;
                body.Position = new Position(50, 700);
                secPage.Paragraphs.Add(body);
            }

            // -----------------------------------------------------------------
            // Insert a Table of Contents page at the beginning
            // -----------------------------------------------------------------
            // Insert a new page at position 1 (the first page)
            Page tocPage = doc.Pages.Insert(1);

            // Title for TOC
            TextFragment tocTitle = new TextFragment("Table of Contents");
            tocTitle.TextState.Font = FontRepository.FindFont("Helvetica");
            tocTitle.TextState.FontSize = 28;
            tocTitle.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGreen;
            tocTitle.Position = new Position(50, 800);
            tocPage.Paragraphs.Add(tocTitle);

            // Add TOC entries with clickable links
            for (int i = 0; i < sectionPages.Length; i++)
            {
                // Y coordinate for each entry (spacing 30 points)
                double yPos = 750 - i * 30;

                // Text for the entry
                string entryText = $"Section {i + 1} ........................................ {sectionPages[i]}";

                TextFragment entry = new TextFragment(entryText);
                entry.TextState.Font = FontRepository.FindFont("Helvetica");
                entry.TextState.FontSize = 14;
                entry.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                entry.Position = new Position(50, yPos);
                tocPage.Paragraphs.Add(entry);

                // Create a link annotation covering the entry text
                // Approximate rectangle: left=50, bottom=yPos-5, right=500, top=yPos+15
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(50, yPos - 5, 500, yPos + 15);
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect);
                // Action navigates to the corresponding section page
                link.Action = new GoToAction(doc.Pages[sectionPages[i]]);
                // Optional visual style for the link (blue underline)
                link.Color = Aspose.Pdf.Color.Blue;
                link.Border = new Border(link) { Width = 0 };
                tocPage.Annotations.Add(link);
            }

            // -----------------------------------------------------------------
            // Save the PDF document – guard against missing GDI+ on non‑Windows platforms
            // -----------------------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).);");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf.Save().");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
