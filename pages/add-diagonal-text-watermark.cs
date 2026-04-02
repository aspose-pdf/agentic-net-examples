using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a sample page with some content
            Page samplePage = doc.Pages.Add();
            samplePage.Paragraphs.Add(new TextFragment("Sample PDF content"));

            // Create a diagonal text watermark (repeating is default in recent Aspose.PDF versions)
            WatermarkArtifact watermark = new WatermarkArtifact
            {
                Text = "CONFIDENTIAL",
                Rotation = 45,
                Opacity = 0.3f,
                IsBackground = true
                // The "Repeat" property was removed in newer versions; the watermark repeats automatically.
            };

            // Define text style for the watermark
            TextState ts = new TextState
            {
                Font = FontRepository.FindFont("Arial"),
                FontSize = 72,
                ForegroundColor = Aspose.Pdf.Color.Gray
            };
            watermark.TextState = ts;

            // Add the watermark to every page in the document
            foreach (Page pg in doc.Pages)
            {
                pg.Artifacts.Add(watermark);
            }

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without rendering the watermark.");
                }
            }
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
