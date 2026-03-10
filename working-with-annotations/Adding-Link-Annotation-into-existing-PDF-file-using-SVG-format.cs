using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Set the required System.Drawing switches *before* any Aspose.Pdf types are touched.
    static Program()
    {
        // Enables System.Drawing on non‑Windows platforms (Linux/macOS).
        // Requires the native libgdiplus package to be installed on the OS.
        AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
        // Forces the use of the legacy GDI+ implementation which works with libgdiplus.
        AppContext.SetSwitch("System.Drawing.UseLegacyGDIPlus", true);
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.svg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Define the rectangle area for the link annotation
                var rect = new Rectangle(100, 500, 300, 550);

                // Create a link annotation that points to an external URL
                var link = new LinkAnnotation(page, rect)
                {
                    Color  = Color.Blue,                     // Optional visual styling
                    Action = new GoToURIAction("https://www.example.com") // Hyperlink action
                };

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(link);

                // Save the modified document as SVG using explicit save options
                var svgOptions = new SvgSaveOptions();
                doc.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"PDF with link annotation saved as SVG to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);
        }
    }
}
