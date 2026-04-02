using System;
using System.Xml;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Sample XML defining page orientation
        string xmlContent = "<Layout><Page orientation=\"landscape\"/></Layout>";
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);
        XmlNode pageNode = xmlDoc.SelectSingleNode("/Layout/Page");
        string orientation = "portrait";
        if (pageNode != null && pageNode.Attributes != null)
        {
            XmlAttribute orientAttr = pageNode.Attributes["orientation"];
            if (orientAttr != null)
            {
                orientation = orientAttr.Value;
            }
        }

        using (Document pdfDocument = new Document())
        {
            // Add a new page
            Page page = pdfDocument.Pages.Add();

            // Set page size (A4) and orientation based on XML attribute
            if (orientation.Equals("landscape", StringComparison.OrdinalIgnoreCase))
            {
                // Landscape: swap width/height
                page.PageInfo.Width = PageSize.A4.Height;
                page.PageInfo.Height = PageSize.A4.Width;
                page.PageInfo.IsLandscape = true;
            }
            else
            {
                page.PageInfo.Width = PageSize.A4.Width;
                page.PageInfo.Height = PageSize.A4.Height;
                page.PageInfo.IsLandscape = false;
            }

            // Add a simple text fragment to indicate the chosen orientation
            TextFragment fragment = new TextFragment("Page orientation: " + orientation);
            fragment.Position = new Position(100, 700);
            page.Paragraphs.Add(fragment);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            const string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDocument.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    pdfDocument.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
