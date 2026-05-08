using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "email_link.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Add visible text indicating the email link
            TextFragment tf = new TextFragment("Contact us: someone@example.com");
            tf.Position = new Position(100, 700); // place near top-left
            page.Paragraphs.Add(tf);

            // Define the rectangle area for the clickable link (covers the text)
            Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 680, 300, 720);

            // Create a link annotation on the page
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            // Set the action to open the default mail client with a mailto: URL
            link.Action = new GoToURIAction("mailto:someone@example.com");
            // Optional visual styling (blue underline)
            link.Color = Aspose.Pdf.Color.Blue;
            // Border must be created after the annotation instance exists
            link.Border = new Border(link) { Width = 0 };

            // Add the annotation to the page
            page.Annotations.Add(link);

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
                                      "The PDF could not be saved using Aspose.Pdf's default renderer.");
                }
            }
        }
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
