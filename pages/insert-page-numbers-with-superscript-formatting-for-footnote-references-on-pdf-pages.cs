using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a sample PDF document
        using (Document document = new Document())
        {
            // Add a page
            Page page = document.Pages.Add();

            // Add normal text
            TextFragment normalText = new TextFragment("This is a sample sentence with a footnote reference");
            page.Paragraphs.Add(normalText);

            // Add a superscript footnote reference (e.g., "1")
            // Superscript is simulated by using a smaller font size. The Rise property is not available in the current Aspose.Pdf version.
            TextFragment footnoteRef = new TextFragment("1");
            footnoteRef.TextState.FontSize = 8f; // smaller font for superscript effect
            page.Paragraphs.Add(footnoteRef);

            // Create a page number stamp. Superscript effect is simulated with a smaller font size.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("#");
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Right;
            pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
            pageNumberStamp.RightMargin = 10f;
            pageNumberStamp.BottomMargin = 10f;
            pageNumberStamp.TextState.FontSize = 8f; // smaller font for superscript effect
            // Note: TextState.Rise is not supported in this version, so we omit it.

            // Apply the stamp to each page in the document
            foreach (Page p in document.Pages)
            {
                p.AddStamp(pageNumberStamp);
            }

            // Save the document (guard against missing GDI+ on non‑Windows platforms)
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(outputPath);
                Console.WriteLine("PDF saved to " + outputPath);
            }
            else
            {
                try
                {
                    document.Save(outputPath);
                    Console.WriteLine("PDF saved to " + outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; cannot save PDF.");
                }
            }
        }
    }

    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}
