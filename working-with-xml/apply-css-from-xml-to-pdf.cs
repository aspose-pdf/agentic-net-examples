using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath   = "input.xml";   // XML (or XHTML) that references external CSS
        const string cssFolder = "styles";      // Folder containing the CSS files
        const string outputPdf = "styled_output.pdf";

        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {xmlPath}");
            return;
        }

        // HtmlLoadOptions constructor that accepts a base path resolves relative URLs
        // (e.g., <link href="style.css" ...>) to the specified folder.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions(Path.GetFullPath(cssFolder));

        try
        {
            // Load the XML/HTML document with the specified options.
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // The document now contains styled content based on the external CSS.
                // Save directly to PDF.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF generated with CSS styling: {outputPdf}");
        }
        catch (TypeInitializationException)
        {
            // HTML-to-PDF conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
