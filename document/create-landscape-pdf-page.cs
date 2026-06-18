using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "landscape.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Rotate the page 90 degrees to achieve landscape orientation
            // NOTE: Aspose.Pdf.Rotation enum values use the 'on' prefix (on90, on180, etc.)
            page.Rotate = Rotation.on90;

            // Optionally, mark the page as landscape (affects page size handling)
            page.PageInfo.IsLandscape = true;

            // Add a sample text fragment to visualize the orientation
            TextFragment tf = new TextFragment("Landscape Page");
            tf.Position = new Position(100, 500); // position within the rotated page
            page.Paragraphs.Add(tf);

            // Save the document as a PDF.
            // Guard the Save call on non‑Windows platforms where libgdiplus may be missing.
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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Saved on non‑Windows platform – ensure libgdiplus is installed if needed.)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF could not be saved.");
                }
            }
        }
    }

    // Helper method to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
