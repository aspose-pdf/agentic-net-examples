using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class PostcardCreator
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "postcard.pdf";

        // 1 inch = 72 points. 5x7 inches => 360 x 504 points
        const double widthPoints  = 5 * 72; // 360
        const double heightPoints = 7 * 72; // 504

        // Create a new PDF document inside a using block (ensures disposal)
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Set the custom page size (5 x 7 inches)
            page.SetPageSize(widthPoints, heightPoints);

            // OPTIONAL: add a sample text to visualize the page
            TextFragment tf = new TextFragment("Hello, Postcard!");
            tf.Position = new Position(50, heightPoints - 50); // 50 pts from left, 50 pts from top
            tf.TextState.FontSize = 24;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            page.Paragraphs.Add(tf);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                }
            }
        }

        Console.WriteLine($"Postcard PDF created at '{Path.GetFullPath(outputPath)}'");
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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