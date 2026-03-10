using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputSvg = "output.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Create a squiggly (jagged underline) text markup annotation on the first page
            Page page = doc.Pages[1];
            // Rectangle defines the annotation bounds (left, bottom, width, height)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);
            SquigglyAnnotation squiggly = new SquigglyAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Red,          // annotation color
                Contents = "Important note",          // popup text
                Title = "Reviewer",                  // title shown in popup
                Opacity = 0.5                          // semi‑transparent
            };

            // Add the annotation to the page
            page.Annotations.Add(squiggly);

            // SVG conversion is only supported on Windows because Aspose.Pdf relies on GDI+.
            // Guard the call with an OS check to avoid the Windows.Win32.PInvoke type‑initializer error on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(outputSvg, svgOptions);
                Console.WriteLine($"SVG file saved to '{outputSvg}'.");
            }
            else
            {
                Console.WriteLine("SVG export is only supported on Windows platforms. Skipping SVG generation.");
            }
        }
    }
}
