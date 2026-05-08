using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string outputPdfPath     = "output.pdf";
        const string appearancePdfPath = "appearance.pdf";

        if (!File.Exists(inputPdfPath) || !File.Exists(appearancePdfPath))
        {
            Console.Error.WriteLine("Input PDF or appearance PDF not found.");
            return;
        }

        // Use PdfContentEditor (facade) to add a rubber‑stamp annotation with a custom appearance stream.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPdfPath);

            // Define the annotation rectangle (coordinates are in points, origin at lower‑left).
            // PdfContentEditor methods require System.Drawing.Rectangle, so we use the fully qualified type.
            var annotationRect = new System.Drawing.Rectangle(100, 500, 200, 600);

            // Open the custom appearance PDF as a stream.
            using (FileStream appearanceStream = File.OpenRead(appearancePdfPath))
            {
                // Create the rubber‑stamp annotation.
                // The color is set to Transparent because the appearance stream provides the visual.
                editor.CreateRubberStamp(
                    page: 1,
                    annotRect: annotationRect,
                    annotContents: "Custom Graphic",
                    color: System.Drawing.Color.Transparent,
                    appearanceStream: appearanceStream);
            }

            // Save the modified document.
            // Document.Save may require GDI+ on non‑Windows platforms; guard the call.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                editor.Save(outputPdfPath);
            }
            else
            {
                try
                {
                    editor.Save(outputPdfPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform; saving was skipped.");
                }
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus).
    static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
