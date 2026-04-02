using System;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class ConditionalPageBreakExample
{
    public static void Main()
    {
        // Path to the XML file
        string xmlPath = "data.xml";

        // Load XML document – use a fallback if the file does not exist
        XmlDocument xmlDoc = new XmlDocument();
        string content = string.Empty;
        if (File.Exists(xmlPath))
        {
            xmlDoc.Load(xmlPath);
            XmlNode contentNode = xmlDoc.SelectSingleNode("//Content");
            content = contentNode != null ? contentNode.InnerText : string.Empty;
        }
        else
        {
            // Fallback XML used when the external file is missing (prevents FileNotFoundException)
            const string fallbackXml = "<Root><Content>This is fallback content used when data.xml is missing. It can be replaced with any default text you need.</Content></Root>";
            xmlDoc.LoadXml(fallbackXml);
            XmlNode contentNode = xmlDoc.SelectSingleNode("//Content");
            content = contentNode != null ? contentNode.InnerText : string.Empty;
        }

        // Define length threshold
        int lengthThreshold = 1000;

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add first page and write the XML content
            Page firstPage = pdfDoc.Pages.Add();
            TextFragment tf = new TextFragment(content);
            firstPage.Paragraphs.Add(tf);

            // If the content exceeds the threshold, insert a page break and add continuation text
            if (content.Length > lengthThreshold)
            {
                Page secondPage = pdfDoc.Pages.Add();
                TextFragment tf2 = new TextFragment("Continued content after page break.");
                secondPage.Paragraphs.Add(tf2);
            }

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDoc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    pdfDoc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform).");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved because Aspose.Pdf requires GDI+ for the Save operation.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException (e.g., missing libgdiplus)
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