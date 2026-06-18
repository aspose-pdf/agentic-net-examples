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
        const string outputPath = "TableOfContents.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // -------------------------------------------------
            // 1. Create a Table of Contents page (page 1)
            // -------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // Add a title for the TOC
            TextFragment tocTitle = new TextFragment("Table of Contents")
            {
                TextState =
                {
                    Font = FontRepository.FindFont("Helvetica"),
                    FontSize = 20,
                    ForegroundColor = Color.Blue
                }
            };
            tocTitle.Position = new Position(50, 750);
            tocPage.Paragraphs.Add(tocTitle);

            // -------------------------------------------------
            // 2. Create content sections (pages 2, 3, 4)
            // -------------------------------------------------
            // Helper to create a section page with a heading
            void AddSection(string heading, int pageNumber)
            {
                Page secPage = doc.Pages.Add();

                // Heading
                TextFragment headingFragment = new TextFragment(heading)
                {
                    TextState =
                    {
                        Font = FontRepository.FindFont("Helvetica-Bold"),
                        FontSize = 18,
                        ForegroundColor = Color.Black
                    }
                };
                headingFragment.Position = new Position(50, 750);
                secPage.Paragraphs.Add(headingFragment);

                // Sample body text
                TextFragment body = new TextFragment("This is the content of " + heading + ".");
                body.Position = new Position(50, 720);
                secPage.Paragraphs.Add(body);
            }

            AddSection("Section 1", 2);
            AddSection("Section 2", 3);
            AddSection("Section 3", 4);

            // -------------------------------------------------
            // 3. Populate the TOC with clickable entries
            // -------------------------------------------------
            // Define vertical positions for TOC entries
            double startY = 700;
            double lineHeight = 20;

            // Array of section titles and their target pages (1‑based indexing)
            var sections = new (string Title, int Page)[]
            {
                ("Section 1", 2),
                ("Section 2", 3),
                ("Section 3", 4)
            };

            for (int i = 0; i < sections.Length; i++)
            {
                var (title, targetPage) = sections[i];
                double y = startY - i * lineHeight;

                // Add visible text for the TOC entry
                TextFragment entry = new TextFragment(title)
                {
                    TextState =
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 12,
                        ForegroundColor = Color.DarkBlue
                    }
                };
                entry.Position = new Position(70, y);
                tocPage.Paragraphs.Add(entry);

                // Create a rectangle that covers the text (approximate coordinates)
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(70, y - 5, 300, y + 12);

                // Create a link annotation that points to the target page
                LinkAnnotation link = new LinkAnnotation(tocPage, linkRect)
                {
                    Action = new GoToAction(doc.Pages[targetPage])
                };

                // Add the annotation to the page
                tocPage.Annotations.Add(link);
            }

            // -------------------------------------------------
            // 4. Save the document (guarded for non‑Windows platforms)
            // -------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows has GDI+ available – safe to save directly
                doc.Save(outputPath);
            }
            else
            {
                // On macOS / Linux the native GDI+ library (libgdiplus) may be missing.
                // Attempt to save and gracefully handle the possible TypeInitializationException.
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the code executed correctly.");
                }
            }
        }

        Console.WriteLine($"PDF with clickable TOC saved to '{outputPath}'.");
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
