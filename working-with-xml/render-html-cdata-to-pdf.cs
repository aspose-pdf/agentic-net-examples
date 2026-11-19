using System;
using System.IO;
using System.Text;
using System.Xml;
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

        // Load the XML document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Create the target PDF document
        using (Document targetPdf = new Document())
        {
            // Iterate over all CDATA sections containing HTML fragments
            var cdataNodes = xmlDoc.SelectNodes("//text()");
            if (cdataNodes != null)
            {
                foreach (XmlNode node in cdataNodes)
                {
                    if (node.NodeType != XmlNodeType.CDATA)
                        continue;

                    string htmlFragment = node.Value;
                    if (string.IsNullOrWhiteSpace(htmlFragment))
                        continue;

                    // Convert the HTML fragment to a temporary PDF document
                    try
                    {
                        using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlFragment)))
                        using (Document htmlDoc = new Document(htmlStream, new HtmlLoadOptions()))
                        {
                            // Append the pages of the temporary document to the target PDF
                            targetPdf.Pages.Add(htmlDoc.Pages);
                        }
                    }
                    catch (TypeInitializationException)
                    {
                        // HTML conversion requires GDI+ (Windows only)
                        Console.WriteLine("HTML rendering skipped on this platform (requires Windows GDI+).");
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error processing HTML fragment: {ex.Message}");
                    }
                }
            }

            // Save the assembled PDF
            targetPdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated: {outputPdf}");
    }
}