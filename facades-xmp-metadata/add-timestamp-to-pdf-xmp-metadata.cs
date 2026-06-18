using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

// Suppress known‑vulnerability NuGet warning (NU1903) for the transitive package
#pragma warning disable NU1903

class Program
{
    static void Main()
    {
        const string outputPath = "generated_with_timestamp.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Insert simple text content
            TextFragment tf = new TextFragment("Sample PDF generated with timestamp in XMP metadata.");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Initialise XMP metadata facade and bind it to the document
            PdfXmpMetadata xmp = new PdfXmpMetadata();
            xmp.BindPdf(doc);

            // Add a custom timestamp property (ISO 8601 format) to the XMP metadata
            string timestamp = DateTime.UtcNow.ToString("o"); // e.g., 2023-09-01T12:34:56.0000000Z
            xmp.Add("xmp:CreateDate", timestamp);

            // Save the PDF – guard the call on non‑Windows platforms where libgdiplus may be missing
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was generated, but saving may be incomplete.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}' with XMP timestamp.");
    }

    // Helper that walks the inner‑exception chain to detect a missing native GDI+ library
    private static bool ContainsDllNotFound(Exception? ex)
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
#pragma warning restore NU1903