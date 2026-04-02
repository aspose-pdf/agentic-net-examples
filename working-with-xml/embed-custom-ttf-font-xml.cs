using System;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create sample XML content
        string xmlContent = "<root><p>Hello from XML</p></root>";
        string xmlPath = "input.xml";
        File.WriteAllText(xmlPath, xmlContent);

        // Load the XML file using standard .NET XML APIs
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);

        // Create a new PDF document
        using (Document pdfDocument = new Document())
        {
            // Ensure that standard fonts are embedded when they are used
            pdfDocument.EmbedStandardFonts = true;

            // Add a blank page to the document
            Page page = pdfDocument.Pages.Add();

            // -------------------------------------------------------------------
            // Embed a custom TrueType font. Replace "MyCustomFont.ttf" with the
            // actual path to the font you want to embed. If the file is not found
            // we fall back to a system font (Arial) so the sample still works.
            // -------------------------------------------------------------------
            string customFontPath = "MyCustomFont.ttf"; // <-- change as needed
            Font customFont;
            if (File.Exists(customFontPath))
                customFont = FontRepository.OpenFont(customFontPath);
            else
                customFont = FontRepository.FindFont("Arial"); // fallback

            // Iterate over the XML <p> nodes and render them as text fragments
            foreach (XmlNode node in xmlDoc.SelectNodes("//p"))
            {
                TextFragment textFragment = new TextFragment(node.InnerText);
                textFragment.TextState.Font = customFont;
                textFragment.TextState.FontSize = 12;
                page.Paragraphs.Add(textFragment);
            }

            string outputPath = "output.pdf";

            // -------------------------------------------------------------------
            // Guard Document.Save for platforms that lack GDI+ (libgdiplus).
            // On Windows the call succeeds directly. On other OSes we try to
            // save and gracefully handle the missing native library.
            // -------------------------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDocument.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    pdfDocument.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        // Clean up temporary XML file
        File.Delete(xmlPath);
    }

    // Helper that walks the inner‑exception chain looking for DllNotFoundException
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
