using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.svg";

        // SVG conversion via Aspose.Pdf relies on Windows‑only GDI+ APIs.
        // Guard against running on unsupported platforms to avoid the
        // "type initializer for 'Windows.Win32.PInvoke'" runtime exception.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.Error.WriteLine("SVG export is supported only on Windows platforms. " +
                                    "Please run this application on a Windows machine or choose a different output format.");
            return;
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (pdfDoc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle where the sticky note will appear
            // (left, bottom, right, top) in points
            Rectangle rect = new Rectangle(100, 500, 200, 550);

            // Create a sticky note (TextAnnotation) with a Comment icon
            TextAnnotation sticky = new TextAnnotation(page, rect)
            {
                Title = "Note",
                Contents = "This is a sticky annotation added via Aspose.Pdf.",
                Color = Color.Yellow,
                Open = true,
                Icon = TextIcon.Comment
            };

            // Add the annotation to the page
            page.Annotations.Add(sticky);

            // Initialise SVG save options (required for non‑PDF output)
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Save the modified PDF as an SVG file
            pdfDoc.Save(outputPath, svgOptions);
        }

        Console.WriteLine($"SVG file saved to '{outputPath}'.");
    }
}

/*
 * NuGet Packages required:
 *   - Aspose.Pdf (>= 23.10 or later)
 *   - System.Drawing.Common (only needed on Windows; Aspose.Pdf uses GDI+ for SVG rendering)
 *
 * Important notes:
 *   1. SVG export is only supported on Windows because Aspose.Pdf internally uses GDI+ APIs.
 *   2. The RuntimeInformation check prevents the "Windows.Win32.PInvoke" type‑initializer
 *      exception that occurs when the code runs on Linux/macOS.
 *   3. Ensure the application targets a Windows‑compatible .NET runtime (e.g., net6.0-windows
 *      or net7.0-windows) to avoid missing native dependencies.
 */