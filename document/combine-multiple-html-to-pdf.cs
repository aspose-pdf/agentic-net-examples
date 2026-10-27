using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Collection of HTML strings to be rendered as PDF pages
        string[] htmlContents = new string[]
        {
            "<html><body><h1>First Page</h1><p>This is the first HTML page.</p></body></html>",
            "<html><body><h2>Second Page</h2><p>Content of the second page.</p></body></html>",
            "<html><body><h3>Third Page</h3><p>More content here.</p></body></html>"
        };

        // Output PDF path
        const string outputPdfPath = "CombinedFromHtml.pdf";

        try
        {
            // Create an empty target document that will hold all pages
            using (Document targetDoc = new Document())
            {
                // Iterate over each HTML string, load it into a temporary Document,
                // then merge its pages into the target document.
                foreach (string html in htmlContents)
                {
                    // Convert the HTML string to a stream (UTF-8 encoding)
                    using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html)))
                    {
                        // Load HTML with default options (no unsupported properties)
                        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

                        // Load the HTML into a temporary Document
                        using (Document tempDoc = new Document(htmlStream, loadOptions))
                        {
                            // Apply desired page size/orientation to each page of the temporary document
                            foreach (Page page in tempDoc.Pages)
                            {
                                // Set A4 size (portrait) using width and height doubles
                                page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);
                                // Ensure portrait orientation
                                page.PageInfo.IsLandscape = false;
                            }

                            // Append all pages from the temporary document to the target
                            targetDoc.Pages.Add(tempDoc.Pages);
                        }
                    }
                }

                // Save the combined PDF
                targetDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF successfully created at '{outputPdfPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML-to-PDF conversion requires GDI+ (Windows only)
            Console.WriteLine("HTML rendering requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
