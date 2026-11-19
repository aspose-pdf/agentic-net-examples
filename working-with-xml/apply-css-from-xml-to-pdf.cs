using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string xmlPath = "input.xml";          // XML (XHTML) file with CSS references
        const string outputPdf = "styled_output.pdf"; // Desired PDF output

        // Verify input file exists
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"File not found: {xmlPath}");
            return;
        }

        try
        {
            // HtmlLoadOptions.BasePath is read‑only; provide the base path via the constructor.
            string basePath = Path.GetDirectoryName(Path.GetFullPath(xmlPath));
            HtmlLoadOptions loadOptions = new HtmlLoadOptions(basePath);

            // Load the XML as an HTML document; CSS will be applied automatically.
            using (Document doc = new Document(xmlPath, loadOptions))
            {
                // Save the styled document as PDF.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF generated successfully: {outputPdf}");
        }
        // HTML/CSS conversion relies on GDI+ and is Windows‑only.
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML/CSS processing requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}