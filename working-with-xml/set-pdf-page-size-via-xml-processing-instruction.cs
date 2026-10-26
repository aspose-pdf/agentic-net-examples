using System;
using System.IO;
using System.Xml;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Parse XML processing instructions
            using (XmlReader reader = XmlReader.Create(xmlPath))
            {
                while (reader.Read())
                {
                    // Look for processing instructions, e.g. <?pdf-page-size A4?>
                    if (reader.NodeType == XmlNodeType.ProcessingInstruction)
                    {
                        string target = reader.Name;   // processing instruction target
                        string data   = reader.Value;  // processing instruction data

                        if (target.Equals("pdf-page-size", StringComparison.OrdinalIgnoreCase))
                        {
                            string value = data.Trim();
                            Page page = pdfDoc.Pages.Add();

                            if (value.StartsWith("Custom:", StringComparison.OrdinalIgnoreCase))
                            {
                                // Parse custom width and height (points)
                                string dims = value.Substring(7);
                                string[] parts = dims.Split(',');
                                if (parts.Length == 2 &&
                                    double.TryParse(parts[0], out double width) &&
                                    double.TryParse(parts[1], out double height))
                                {
                                    page.PageInfo.Width  = width;
                                    page.PageInfo.Height = height;
                                }
                                else
                                {
                                    Console.Error.WriteLine($"Invalid custom size format: {value}");
                                }
                            }
                            else
                            {
                                // Map to a predefined PageSize static field (A4, Letter, etc.)
                                var sizeField = typeof(PageSize).GetField(value,
                                    BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
                                if (sizeField != null && sizeField.GetValue(null) is PageSize size)
                                {
                                    page.PageInfo.Width  = size.Width;
                                    page.PageInfo.Height = size.Height;
                                }
                                else
                                {
                                    Console.Error.WriteLine($"Unknown page size: {value}");
                                }
                            }

                            // Add a simple paragraph to visualize the page
                            page.Paragraphs.Add(new TextFragment($"Page with size: {value}"));
                        }
                    }
                }
            }

            // Save the PDF document
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF generated: {outputPdf}");
    }
}
