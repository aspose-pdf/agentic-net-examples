using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // Cross‑platform Color

class Program
{
    // Set the required switches **before** any Aspose.Pdf type is touched.
    static Program()
    {
        // Enables System.Drawing on non‑Windows platforms (required for SVG output).
        // Must be called before the first use of any Aspose.Pdf class.
        AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);
        // Optional: forces the use of the legacy GDI+ implementation which works better on Linux.
        AppContext.SetSwitch("System.Drawing.UseLegacyGDIPlus", true);
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "output.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Ensure the document has at least one page
                if (pdfDoc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The PDF has no pages.");
                    return;
                }

                // Get the first page (1‑based indexing)
                Page page = pdfDoc.Pages[1];

                // Define the rectangle for the figure annotation.
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a square (figure) annotation on the page.
                SquareAnnotation square = new SquareAnnotation(page, rect)
                {
                    // Use Aspose.Pdf.Drawing.Color for cross‑platform compatibility.
                    Color    = Color.Red,
                    Contents = "This is a figure annotation.",
                    Title    = "Figure"
                };

                // Add the annotation to the page's annotation collection.
                page.Annotations.Add(square);

                // Save the modified document as SVG.
                // SvgSaveOptions is in the Aspose.Pdf namespace (no separate sub‑namespace).
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                pdfDoc.Save(outputSvgPath, svgOptions);
            }

            Console.WriteLine($"Figure annotation added and PDF saved as SVG to '{outputSvgPath}'.");
        }
        catch (Exception ex)
        {
            // Provide a clearer message when System.Drawing cannot be used (e.g., on non‑Windows platforms).
            Console.Error.WriteLine("An error occurred while processing the PDF: " + ex.Message);
            Console.Error.WriteLine("If you are running on Linux/macOS, ensure the 'System.Drawing.Common' NuGet package is referenced and the required AppContext switches are set before any Aspose.Pdf type is used.");
        }
    }
}
