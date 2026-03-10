using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Color

class Program
{
    static void Main()
    {
        // IMPORTANT: Enable System.Drawing support on non‑Windows platforms BEFORE any Aspose.Pdf types are touched.
        // This switch must be the very first line executed in the process.
        AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);

        // Input PDF path – replace with your source file
        const string inputPdf = "input.pdf";
        // Output SVG path
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Ensure the document has at least one page
                if (doc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The PDF has no pages.");
                    return;
                }

                // Get the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the rectangle area for the link annotation
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create the link annotation on the specified page and rectangle
                LinkAnnotation link = new LinkAnnotation(page, linkRect)
                {
                    // Set a visible border color (optional)
                    Color = Color.Blue
                };

                // Assign an external URL using GoToURIAction (Hyperlink is not a string)
                link.Action = new GoToURIAction("https://www.example.com");

                // Add the annotation to the page's annotation collection
                page.Annotations.Add(link);

                // Prepare SVG save options (required for non‑PDF output)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the modified document as SVG
                doc.Save(outputSvg, svgOptions);
            }

            Console.WriteLine($"PDF with link annotation saved as SVG: '{outputSvg}'.");
        }
        catch (Aspose.Pdf.PdfException ex)
        {
            // Provide a clearer message – on Linux/macOS the native libgdiplus library must be present.
            Console.Error.WriteLine("Aspose.Pdf failed: " + ex.Message);
            Console.Error.WriteLine("If you are running on a non‑Windows OS, ensure that the 'libgdiplus' native library is installed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Unexpected error: " + ex.Message);
        }
    }
}
