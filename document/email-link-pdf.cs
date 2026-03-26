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

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Add visible text showing the email address
            TextFragment tf = new TextFragment("Contact us at support@example.com");
            tf.Position = new Position(100, 700);
            page.Paragraphs.Add(tf);

            // Define a rectangle that covers the text (adjust as needed)
            var linkRect = new Aspose.Pdf.Rectangle(100, 680, 300, 720);

            // Create a link annotation that opens the default mail client
            LinkAnnotation link = new LinkAnnotation(page, linkRect);
            link.Action = new GoToURIAction("mailto:support@example.com");
            link.Color = Aspose.Pdf.Color.Blue;
            // Remove the border for a clean look
            link.Border = new Border(link) { Width = 0 };
            page.Annotations.Add(link);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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