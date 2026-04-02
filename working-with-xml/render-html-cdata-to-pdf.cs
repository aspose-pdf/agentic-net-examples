using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for HtmlFragment

namespace RenderHtmlCdataToPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample XML containing HTML fragments inside CDATA sections
            string xmlContent = "<root><section><![CDATA[<html><body><h1>Hello World</h1><p>This is a paragraph from CDATA.</p></body></html>]]></section><section><![CDATA[<html><body><h2>Second Fragment</h2><ul><li>Item 1</li><li>Item 2</li></ul></body></html>]]></section></root>";

            // Load XML and extract CDATA (HTML) fragments
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new MemoryStream(Encoding.UTF8.GetBytes(xmlContent)));
            XmlNodeList cdataNodes = xmlDoc.GetElementsByTagName("section");

            // Prepare the final PDF document
            using (Document finalDoc = new Document())
            {
                // Remove the default empty page that Aspose creates
                finalDoc.Pages.Clear();

                foreach (XmlNode node in cdataNodes)
                {
                    // The InnerText of the node contains the HTML fragment
                    string htmlFragment = node.InnerText;

                    // Create a new page for each fragment
                    Page page = finalDoc.Pages.Add();

                    // Add the HTML fragment directly to the page using HtmlFragment
                    HtmlFragment html = new HtmlFragment(htmlFragment);
                    page.Paragraphs.Add(html);
                }

                // Save the merged PDF – guard the call on non‑Windows platforms where libgdiplus may be missing
                string outputPath = "output.pdf";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    finalDoc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                else
                {
                    try
                    {
                        finalDoc.Save(outputPath);
                        Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus present)");
                    }
                    catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                    {
                        Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
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
}
