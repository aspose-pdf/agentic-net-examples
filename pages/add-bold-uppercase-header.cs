using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Add sample body text
            TextFragment body = new TextFragment("This is a sample PDF document.");
            page.Paragraphs.Add(body);

            // Create header text fragment (bold and uppercase)
            TextFragment headerFragment = new TextFragment("SECTION HEADING");
            headerFragment.TextState.Font = FontRepository.FindFont("Arial");
            headerFragment.TextState.FontSize = 12;
            headerFragment.TextState.FontStyle = FontStyles.Bold;
            headerFragment.TextState.ForegroundColor = Color.Black;

            // Create HeaderFooter and assign the header fragment
            HeaderFooter header = new HeaderFooter();
            header.Paragraphs.Add(headerFragment);
            header.Margin = new MarginInfo(0, 0, 20, 0); // top margin

            // Apply the header to the current page (and any future pages you add manually)
            page.Header = header;

            // Save the document with a guard for platforms lacking GDI+ (libgdiplus)
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
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).");
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
