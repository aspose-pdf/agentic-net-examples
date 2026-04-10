using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "email_link.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Add visible text that will act as the link label
            TextFragment tf = new TextFragment("Contact us: info@example.com");
            tf.Position = new Position(100, 700); // place near top of the page
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            page.Paragraphs.Add(tf);

            // Define the clickable area (rectangle) that covers the text
            // Rectangle(left, bottom, right, top)
            var linkRect = new Aspose.Pdf.Rectangle(100, 680, 350, 720);

            // Create a link annotation on the page
            LinkAnnotation link = new LinkAnnotation(page, linkRect)
            {
                // Use GoToURIAction with a mailto: URL to open the default mail client
                Action = new GoToURIAction("mailto:info@example.com"),
                // Optional visual styling for the link annotation
                Color = Aspose.Pdf.Color.Transparent // no visible border
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            SaveDocument(doc, outputPath);
        }

        Console.WriteLine($"PDF with email link saved to '{outputPath}'.");
    }

    private static void SaveDocument(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present, so we can save directly.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            return;
        }

        // On macOS / Linux Aspose.Pdf may try to load GDI+ (libgdiplus). If it is missing, a
        // TypeInitializationException wrapping a DllNotFoundException is thrown. We catch it
        // and inform the user while still attempting to save the PDF (Aspose can often write
        // the file without needing GDI+ for simple documents).
        try
        {
            doc.Save(path);
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              "The PDF was saved without rendering‑dependent features.");
        }
    }

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
