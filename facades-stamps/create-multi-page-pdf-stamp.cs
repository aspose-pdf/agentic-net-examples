using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF that will receive the stamps
        const string targetPdf = "target.pdf";

        // Paths to the PDF files that will be used as stamps (each may have multiple pages)
        // For this example we assume each stamp PDF has at least one page and we use its first page.
        var stampPdfFiles = new List<string>
        {
            "stamp1.pdf",
            "stamp2.pdf",
            "stamp3.pdf"
        };

        // Output file that will contain the stamped document
        const string outputPdf = "stamped_output.pdf";

        // Verify that all files exist before proceeding
        if (!File.Exists(targetPdf))
        {
            Console.Error.WriteLine($"Target PDF not found: {targetPdf}");
            return;
        }

        foreach (var sp in stampPdfFiles)
        {
            if (!File.Exists(sp))
            {
                Console.Error.WriteLine($"Stamp PDF not found: {sp}");
                return;
            }
        }

        // Create the PdfFileStamp facade and bind the target document
        var fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(targetPdf); // loads the document to be stamped

        // Create a Stamp for each source PDF and add it to the facade
        foreach (var stampPath in stampPdfFiles)
        {
            // Create a new Stamp instance
            var stamp = new Stamp();

            // Bind the first page of the stamp PDF as the stamp content
            // (page numbers are 1‑based)
            stamp.BindPdf(stampPath, 1);

            // Optional: make the stamp appear as a background element
            stamp.IsBackground = true;

            // Optional: set opacity (0.0 = fully transparent, 1.0 = fully opaque)
            stamp.Opacity = 0.8f;

            // Optional: position the stamp (origin is measured from the lower‑left corner)
            stamp.SetOrigin(0, 0); // place at the lower‑left corner; adjust as needed

            // Add the configured stamp to the PdfFileStamp object
            fileStamp.AddStamp(stamp);
        }

        // Save the result. PdfFileStamp.Save writes the output file.
        // On non‑Windows platforms Aspose.Pdf may require libgdiplus for rendering.
        // Guard the save operation to avoid TypeInitializationException on macOS/Linux.
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fileStamp.Save(outputPdf);
            }
            else
            {
                // Attempt to save; if GDI+ is missing the exception will be caught below.
                fileStamp.Save(outputPdf);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              "The stamping operation could not be completed.");
        }
        finally
        {
            // Close releases any resources held by the facade.
            fileStamp.Close();
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
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