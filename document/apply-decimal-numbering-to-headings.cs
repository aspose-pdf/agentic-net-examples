using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output paths
        const string outputPath = "NumberedHeadings.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Create Heading 1 (Level 1) – will be numbered automatically
            // -----------------------------------------------------------------
            Heading heading1 = new Heading(1)               // Level = 1
            {
                Text = "Chapter 1: Introduction",
                IsAutoSequence = true,                     // Enable automatic numbering
                Level = 1,                                 // Hierarchical level
                Style = NumberingStyle.NumeralsArabic,    // Decimal (Arabic) numbering
                // Optional: start numbering at a specific value
                // StartNumber = 1,
                TextState = { FontSize = 20, Font = FontRepository.FindFont("Helvetica-Bold") }
            };
            // Position the heading on the page
            heading1.Position = new Position(50, 750);
            page.Paragraphs.Add(heading1);

            // -----------------------------------------------------------------
            // Create Heading 2 (Level 2) – sub‑section of Chapter 1
            // -----------------------------------------------------------------
            Heading heading2 = new Heading(2)               // Level = 2
            {
                Text = "Overview",
                IsAutoSequence = true,
                Level = 2,
                Style = NumberingStyle.NumeralsArabic,
                TextState = { FontSize = 16, Font = FontRepository.FindFont("Helvetica-Bold") }
            };
            heading2.Position = new Position(70, 720);
            page.Paragraphs.Add(heading2);

            // -----------------------------------------------------------------
            // Create Heading 3 (Level 2) – another sub‑section of Chapter 1
            // -----------------------------------------------------------------
            Heading heading3 = new Heading(2)
            {
                Text = "Scope",
                IsAutoSequence = true,
                Level = 2,
                Style = NumberingStyle.NumeralsArabic,
                TextState = { FontSize = 16, Font = FontRepository.FindFont("Helvetica-Bold") }
            };
            heading3.Position = new Position(70, 690);
            page.Paragraphs.Add(heading3);

            // -----------------------------------------------------------------
            // Create Heading 4 (Level 1) – second top‑level chapter
            // -----------------------------------------------------------------
            Heading heading4 = new Heading(1)
            {
                Text = "Methodology",
                IsAutoSequence = true,
                Level = 1,
                Style = NumberingStyle.NumeralsArabic,
                // StartNumber can be set to continue numbering after previous chapter
                // StartNumber = 2,
                TextState = { FontSize = 20, Font = FontRepository.FindFont("Helvetica-Bold") }
            };
            heading4.Position = new Position(50, 650);
            page.Paragraphs.Add(heading4);

            // -----------------------------------------------------------------
            // Create Heading 5 (Level 2) – sub‑section of Chapter 2
            // -----------------------------------------------------------------
            Heading heading5 = new Heading(2)
            {
                Text = "Data Collection",
                IsAutoSequence = true,
                Level = 2,
                Style = NumberingStyle.NumeralsArabic,
                TextState = { FontSize = 16, Font = FontRepository.FindFont("Helvetica-Bold") }
            };
            heading5.Position = new Position(70, 620);
            page.Paragraphs.Add(heading5);

            // -----------------------------------------------------------------
            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, GDI+ may be missing but save succeeded)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF could not be saved.");
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
