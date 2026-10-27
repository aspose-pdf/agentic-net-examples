using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Create an empty PDF document to collect rendered pages
        using (Document outputDoc = new Document())
        {
            // Load the XML document
            XDocument xdoc = XDocument.Load(xmlPath);

            // Find all CDATA sections (they may contain HTML fragments)
            var cdataNodes = xdoc.DescendantNodes().OfType<XCData>();

            foreach (var cdata in cdataNodes)
            {
                string html = cdata.Value;

                // Render each HTML fragment to PDF pages
                try
                {
                    // Load HTML from a memory stream using HtmlLoadOptions (Windows GDI+ required)
                    using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
                    using (Document htmlDoc = new Document(htmlStream, new HtmlLoadOptions()))
                    {
                        // Append each page from the temporary HTML document to the output PDF
                        foreach (Page page in htmlDoc.Pages)
                        {
                            outputDoc.Pages.Add(page);
                        }
                    }
                }
                catch (TypeInitializationException)
                {
                    // HTML rendering is Windows‑only; skip on other platforms
                    Console.WriteLine("HTML rendering requires Windows (GDI+). Skipping this fragment.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error rendering HTML fragment: {ex.Message}");
                }
            }

            // Save the combined PDF
            outputDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created: {outputPdf}");
    }
}