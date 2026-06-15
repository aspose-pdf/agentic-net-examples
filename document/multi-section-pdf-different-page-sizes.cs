using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_section.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // ---------- Section 1: A4 portrait ----------
            Page page1 = doc.Pages.Add();
            page1.PageInfo.Width = PageSize.A4.Width;
            page1.PageInfo.Height = PageSize.A4.Height;
            page1.PageInfo.IsLandscape = false;
            AddSectionContent(page1, "Section 1: A4 Portrait");

            // ---------- Section 2: A4 landscape ----------
            Page page2 = doc.Pages.Add();
            page2.PageInfo.Width = PageSize.A4.Height; // swap for landscape
            page2.PageInfo.Height = PageSize.A4.Width;
            page2.PageInfo.IsLandscape = true;
            AddSectionContent(page2, "Section 2: A4 Landscape");

            // ---------- Section 3: Letter portrait ----------
            Page page3 = doc.Pages.Add();
            page3.PageInfo.Width = PageSize.PageLetter.Width;
            page3.PageInfo.Height = PageSize.PageLetter.Height;
            page3.PageInfo.IsLandscape = false;
            AddSectionContent(page3, "Section 3: Letter Portrait");

            // Save the document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                }
            }
        }

        Console.WriteLine($"PDF created at '{outputPath}'.");
    }

    // Helper method to add a simple text fragment to a page
    static void AddSectionContent(Page page, string text)
    {
        TextFragment tf = new TextFragment(text);
        tf.Position = new Position(100, 700); // Position near top-left
        tf.TextState.FontSize = 24;
        tf.TextState.Font = FontRepository.FindFont("Helvetica");
        tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
        page.Paragraphs.Add(tf);
    }

    // Detect a nested DllNotFoundException (e.g., missing libgdiplus)
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