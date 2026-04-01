using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        const string outputPath = "timestamped.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Add sample text to the page
            TextFragment text = new TextFragment("Sample PDF with timestamped XMP metadata.");
            page.Paragraphs.Add(text);

            // Initialize XMP metadata facade bound to the document
            PdfXmpMetadata xmp = new PdfXmpMetadata(doc);

            // Create an ISO‑8601 UTC timestamp
            string timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            // Add the timestamp as the ModifyDate property in XMP metadata
            xmp.Add("xmp:ModifyDate", timestamp);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "PDF was not saved. Install libgdiplus or run on Windows.");
                }
            }
        }
    }

    // Helper to walk the inner‑exception chain and detect a missing native library
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
