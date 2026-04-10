using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "HeadingsWithDecimalNumbering.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Heading level 1
            Aspose.Pdf.Heading heading1 = new Aspose.Pdf.Heading(1)
            {
                Text = "Chapter 1: Introduction",
                IsAutoSequence = true,                         // Enable automatic numbering
                Style = Aspose.Pdf.NumberingStyle.NumeralsArabic // Decimal (Arabic) numbering
            };
            // Optional: set font and size
            heading1.TextState.Font = FontRepository.FindFont("Helvetica");
            heading1.TextState.FontSize = 20;
            page.Paragraphs.Add(heading1);

            // Heading level 2 under Chapter 1
            Aspose.Pdf.Heading heading2 = new Aspose.Pdf.Heading(2)
            {
                Text = "Section 1.1: Background",
                IsAutoSequence = true,
                Style = Aspose.Pdf.NumberingStyle.NumeralsArabic
            };
            heading2.TextState.Font = FontRepository.FindFont("Helvetica");
            heading2.TextState.FontSize = 16;
            page.Paragraphs.Add(heading2);

            // Another level 2 heading
            Aspose.Pdf.Heading heading3 = new Aspose.Pdf.Heading(2)
            {
                Text = "Section 1.2: Objectives",
                IsAutoSequence = true,
                Style = Aspose.Pdf.NumberingStyle.NumeralsArabic
            };
            heading3.TextState.Font = FontRepository.FindFont("Helvetica");
            heading3.TextState.FontSize = 16;
            page.Paragraphs.Add(heading3);

            // Heading level 3 under Section 1.2
            Aspose.Pdf.Heading heading4 = new Aspose.Pdf.Heading(3)
            {
                Text = "Subsection 1.2.1: Scope",
                IsAutoSequence = true,
                Style = Aspose.Pdf.NumberingStyle.NumeralsArabic
            };
            heading4.TextState.Font = FontRepository.FindFont("Helvetica");
            heading4.TextState.FontSize = 14;
            page.Paragraphs.Add(heading4);

            // Add a second top‑level chapter to demonstrate continuation of numbering
            Aspose.Pdf.Heading heading5 = new Aspose.Pdf.Heading(1)
            {
                Text = "Chapter 2: Methodology",
                IsAutoSequence = true,
                Style = Aspose.Pdf.NumberingStyle.NumeralsArabic
            };
            heading5.TextState.Font = FontRepository.FindFont("Helvetica");
            heading5.TextState.FontSize = 20;
            page.Paragraphs.Add(heading5);

            // Save the document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
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