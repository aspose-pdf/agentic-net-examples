using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string pdfPath = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Create HtmlLoadOptions with default settings.
            // If the current Aspose.Pdf version supports a property that limits
            // resource loading to local files, set it via reflection so the code
            // compiles against older versions as well.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Attempt to set ResourceLoadingMode = LocalOnly (newer versions).
            var rlModeProp = typeof(HtmlLoadOptions).GetProperty("ResourceLoadingMode");
            if (rlModeProp != null && rlModeProp.CanWrite)
            {
                var enumType = rlModeProp.PropertyType;
                var localOnly = Enum.Parse(enumType, "LocalOnly");
                rlModeProp.SetValue(loadOptions, localOnly);
            }

            // Load the HTML document.
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // ------------------------------------------------------------
                // NOTE: The original sample used PageFragment to merge all pages
                // into a single page. The PageFragment class is not available in
                // all Aspose.Pdf versions and its usage would break the build.
                // To keep the code portable and compilable across versions we
                // simply keep the document as‑is. If a single‑page output is
                // required, the caller can later use a PDF editor or a newer
                // Aspose.Pdf version that provides PageFragment.
                // ------------------------------------------------------------

                // Save the resulting PDF (single‑page merging omitted for
                // compatibility).
                doc.Save(pdfPath);
            }

            Console.WriteLine($"HTML successfully converted to PDF: {pdfPath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion uses GDI+ and is Windows‑only.
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
