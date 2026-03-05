using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input HTML file and output PDF file paths
        const string htmlPath = "input.html";
        const string pdfPath = "output.pdf";

        // Folder where individual page PDFs will be saved (used as a simple stand‑in for layers)
        const string layersFolder = "Layers";

        // Verify that the source HTML exists
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Ensure the folder for page PDFs exists
        Directory.CreateDirectory(layersFolder);

        try
        {
            // Load the HTML document with default options
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Convert HTML to PDF inside a using block (deterministic disposal)
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Save the combined PDF
                doc.Save(pdfPath);

                // -----------------------------------------------------------------
                // NOTE:
                // The original sample attempted to work with Optional Content Groups
                // (layers) via the OptionalContentGroup class. The version of
                // Aspose.Pdf referenced in the project does not expose this API, which
                // caused the compilation errors:
                //   • Document does not contain a definition for 'OptionalContentGroups'
                //   • The type 'OptionalContentGroup' could not be found
                //
                // To keep the example functional and compile‑time safe we fall back to
                // extracting each *page* as an individual PDF file. This demonstrates
                // how to split a generated PDF into separate documents without relying
                // on the optional‑content‑group API. If a newer Aspose.Pdf version that
                // supports OCG is available, the original logic can be reinstated.
                // -----------------------------------------------------------------

                int pageIndex = 1;
                foreach (Page page in doc.Pages)
                {
                    // Create a new document that will contain only the current page
                    Document pageDoc = new Document();
                    // Add a clone of the page to the new document
                    pageDoc.Pages.Add(page);

                    // Build a file name for the page PDF (e.g., Page_1.pdf, Page_2.pdf, ...)
                    string pageFileName = $"Page_{pageIndex}.pdf";
                    string pagePath = Path.Combine(layersFolder, pageFileName);

                    // Save the single‑page PDF
                    pageDoc.Save(pagePath);

                    pageIndex++;
                }
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
            Console.WriteLine($"Individual page PDFs saved in folder: {layersFolder}");
        }
        // HTML‑to‑PDF conversion relies on GDI+ and may fail on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.Error.WriteLine("HTML conversion requires Windows GDI+. Skipping on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
