using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Heading and NumberingStyle are defined here

class Program
{
    static void Main()
    {
        const string outputPath = "numbered_headings.pdf";

        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Level 1 heading with automatic decimal numbering
            Heading heading1 = new Heading(1)
            {
                Text = "Chapter 1",
                IsAutoSequence = true,
                Style = NumberingStyle.NumeralsArabic // decimal style
            };
            page.Paragraphs.Add(heading1);

            // Level 2 heading
            Heading heading2 = new Heading(2)
            {
                Text = "Section 1.1",
                IsAutoSequence = true,
                Style = NumberingStyle.NumeralsArabic
            };
            page.Paragraphs.Add(heading2);

            // Another Level 2 heading
            Heading heading2b = new Heading(2)
            {
                Text = "Section 1.2",
                IsAutoSequence = true,
                Style = NumberingStyle.NumeralsArabic
            };
            page.Paragraphs.Add(heading2b);

            // Level 3 heading
            Heading heading3 = new Heading(3)
            {
                Text = "Subsection 1.2.1",
                IsAutoSequence = true,
                Style = NumberingStyle.NumeralsArabic
            };
            page.Paragraphs.Add(heading3);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
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
