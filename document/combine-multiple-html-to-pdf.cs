using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Collection of HTML strings to be rendered into the PDF
        string[] htmlPages = new string[]
        {
            "<html><body><h1>First Page</h1><p>Hello, world!</p></body></html>",
            "<html><body><h1>Second Page</h1><p>Another paragraph.</p></body></html>",
            "<html><body><h1>Third Page</h1><p>More content here.</p></body></html>"
        };

        const string outputPdfPath = "CombinedFromHtml.pdf";

        // Target PDF document that will contain all rendered pages
        using (Document target = new Document())
        {
            // Iterate over each HTML string, render it to a temporary PDF document,
            // then merge its pages into the target document.
            foreach (string html in htmlPages)
            {
                // Convert the HTML string to a stream for loading
                using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
                {
                    // Custom rendering options for HTML loading
                    HtmlLoadOptions loadOptions = new HtmlLoadOptions
                    {
                        // Set a fixed page size (A4). Width and Height are in points.
                        PageInfo = new PageInfo
                        {
                            Width = 595,   // 8.27 inches
                            Height = 842   // 11.69 inches
                        }
                        // Note: The 'FixedLayout' property does not exist in the current
                        // Aspose.Pdf version and has been removed.
                    };

                    // Load the HTML into a temporary PDF document
                    // Wrap in try-catch because HTML conversion may require GDI+ (Windows only)
                    try
                    {
                        using (Document source = new Document(htmlStream, loadOptions))
                        {
                            // Append all pages from the source document to the target
                            target.Pages.Add(source.Pages);
                        }
                    }
                    catch (TypeInitializationException)
                    {
                        Console.WriteLine("HTML rendering requires Windows (GDI+). Skipping this HTML segment.");
                    }
                }
            }

            // Save the combined PDF. No SaveOptions needed because the output format is PDF.
            target.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created at '{outputPdfPath}'.");
    }
}