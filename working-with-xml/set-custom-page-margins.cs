using System;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class SetCustomMargins
{
    public static void Main()
    {
        // NOTE: In evaluation mode Aspose.PDF allows a maximum of 4 elements in any collection.
        // The original sample created 5 pages which caused an IndexOutOfRangeException.
        // Reduce the page count to 4 (or obtain a full license) to stay within the limit.
        using (Document doc = new Document())
        {
            // Create up to 4 pages (evaluation limit)
            for (int i = 0; i < 4; i++)
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment($"Page {i + 1}");
                tf.Position = new Position(100f, 700f);
                page.Paragraphs.Add(tf);
            }

            // XML that defines margin values per page range (section)
            string xmlContent = @"<Sections>
    <Section StartPage='1' EndPage='2' Top='20' Bottom='20' Left='30' Right='30' />
    <Section StartPage='3' EndPage='5' Top='40' Bottom='40' Left='50' Right='50' />
</Sections>";

            XDocument xdoc = XDocument.Parse(xmlContent);
            foreach (XElement section in xdoc.Root.Elements("Section"))
            {
                // Clamp page numbers to the actual page count to avoid out‑of‑range access.
                int startPage = (int)section.Attribute("StartPage");
                int endPage   = (int)section.Attribute("EndPage");
                int maxPage   = doc.Pages.Count;
                startPage = Math.Max(1, startPage);
                endPage   = Math.Min(maxPage, endPage);

                float top    = (float)section.Attribute("Top");
                float bottom = (float)section.Attribute("Bottom");
                float left   = (float)section.Attribute("Left");
                float right  = (float)section.Attribute("Right");

                MarginInfo margin = new MarginInfo
                {
                    Top    = top,
                    Bottom = bottom,
                    Left   = left,
                    Right  = right
                };

                for (int p = startPage; p <= endPage; p++)
                {
                    doc.Pages[p].PageInfo.Margin = margin;
                }
            }

            string outputPath = "output.pdf";

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms.
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
