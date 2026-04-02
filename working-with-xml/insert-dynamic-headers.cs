using System;
using System.IO;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class InsertDynamicHeaders
{
    public static void Main()
    {
        // XML that defines a header for each page
        string xmlContent = @"<Headers>
    <Header page='1'>First Page Header</Header>
    <Header page='2'>Second Page Header</Header>
    <Header page='3'>Third Page Header</Header>
</Headers>";

        // Load XML into XDocument for quick lookup
        XDocument xDocument = XDocument.Parse(xmlContent);

        // Create a new PDF document with three pages
        using (Document document = new Document())
        {
            // Add three blank pages
            document.Pages.Add();
            document.Pages.Add();
            document.Pages.Add();

            // Iterate over each page and insert the appropriate header
            for (int i = 1; i <= document.Pages.Count; i++)
            {
                Page page = document.Pages[i];
                int currentPageNumber = page.Number;

                // Find the header text for the current page from the XML
                XElement headerElement = null;
                foreach (XElement element in xDocument.Root.Elements("Header"))
                {
                    XAttribute attr = element.Attribute("page");
                    if (attr != null && attr.Value == currentPageNumber.ToString())
                    {
                        headerElement = element;
                        break;
                    }
                }

                if (headerElement != null)
                {
                    string headerText = headerElement.Value;

                    // Create a text fragment for the header
                    TextFragment headerFragment = new TextFragment(headerText);
                    headerFragment.TextState.Font = FontRepository.FindFont("Arial");
                    headerFragment.TextState.FontSize = 12;
                    headerFragment.TextState.FontStyle = FontStyles.Bold;

                    // Position the header near the top centre of the page
                    double pageWidth = page.PageInfo.Width;
                    double headerY = page.PageInfo.Height - 20; // 20 points from top edge
                    headerFragment.Position = new Position(pageWidth / 2, headerY);
                    headerFragment.HorizontalAlignment = HorizontalAlignment.Center;

                    // Add the fragment to the page
                    page.Paragraphs.Add(headerFragment);
                }
            }

            // Save the resulting PDF – guard against missing GDI+ on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    document.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, GDI+ may be missing but save succeeded)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "Saving the PDF without GDI+ dependent features is not possible. " +
                                      "Consider installing libgdiplus or running on Windows.");
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
