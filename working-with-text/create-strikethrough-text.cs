using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "strikethrough.pdf";

        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a blank page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the desired content
            TextFragment tf = new TextFragment("This text will be struck through");

            // Enable strikeout via the TextState
            tf.TextState.StrikeOut = true;

            // Optional styling
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Color.Black;

            // Position the fragment on the page (baseline coordinates)
            tf.Position = new Position(100, 700); // X = 100, Y = 700

            // Add the fragment to the page
            page.Paragraphs.Add(tf);

            // Save the document with a guard for platforms that lack GDI+ (libgdiplus)
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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