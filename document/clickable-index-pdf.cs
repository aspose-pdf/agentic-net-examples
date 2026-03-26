using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "clickable_index.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // ---------- Section 1 page (page 2) ----------
            Page section1Page = doc.Pages.Add();
            TextFragment heading1 = new TextFragment("Section 1 – Introduction");
            heading1.TextState.FontSize = 18;
            heading1.Position = new Position(50, 750);
            section1Page.Paragraphs.Add(heading1);
            // Sample content
            TextFragment content1 = new TextFragment("This is the introduction section of the document.");
            content1.Position = new Position(50, 720);
            section1Page.Paragraphs.Add(content1);

            // ---------- Section 2 page (page 3) ----------
            Page section2Page = doc.Pages.Add();
            TextFragment heading2 = new TextFragment("Section 2 – Details");
            heading2.TextState.FontSize = 18;
            heading2.Position = new Position(50, 750);
            section2Page.Paragraphs.Add(heading2);
            // Sample content
            TextFragment content2 = new TextFragment("This section provides detailed information.");
            content2.Position = new Position(50, 720);
            section2Page.Paragraphs.Add(content2);

            // ---------- Index page (page 1) ----------
            Page indexPage = doc.Pages.Insert(1); // Insert at the beginning so it becomes page 1
            // Title for index page
            TextFragment indexTitle = new TextFragment("Table of Contents");
            indexTitle.TextState.FontSize = 20;
            indexTitle.Position = new Position(50, 750);
            indexPage.Paragraphs.Add(indexTitle);

            // First entry – Section 1
            double entry1Left = 50.0;
            double entry1Bottom = 700.0;
            double entry1Right = 300.0;
            double entry1Top = 720.0;
            TextFragment entry1 = new TextFragment("1. Introduction");
            entry1.Position = new Position(entry1Left, entry1Bottom + 10);
            indexPage.Paragraphs.Add(entry1);
            LinkAnnotation link1 = new LinkAnnotation(indexPage, new Aspose.Pdf.Rectangle(entry1Left, entry1Bottom, entry1Right, entry1Top));
            link1.Action = new GoToAction(doc.Pages[2]);
            indexPage.Annotations.Add(link1);

            // Second entry – Section 2
            double entry2Left = 50.0;
            double entry2Bottom = 660.0;
            double entry2Right = 300.0;
            double entry2Top = 680.0;
            TextFragment entry2 = new TextFragment("2. Details");
            entry2.Position = new Position(entry2Left, entry2Bottom + 10);
            indexPage.Paragraphs.Add(entry2);
            LinkAnnotation link2 = new LinkAnnotation(indexPage, new Aspose.Pdf.Rectangle(entry2Left, entry2Bottom, entry2Right, entry2Top));
            link2.Action = new GoToAction(doc.Pages[3]);
            indexPage.Annotations.Add(link2);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF with clickable index saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping PDF save – libgdiplus (GDI+) is required on non‑Windows platforms.");
            }
        }
    }
}
