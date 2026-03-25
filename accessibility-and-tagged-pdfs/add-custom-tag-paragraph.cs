using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "custom_tag_paragraph.pdf";

        using (Document doc = new Document())
        {
            // Initialize tagged content and set document metadata
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Custom Tag Example");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element, set its text and a custom tag
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("This paragraph represents a specialized content type.");
            paragraph.SetTag("MySpecialParagraph"); // custom tag name

            // Attach the paragraph to the document's structure tree
            root.AppendChild(paragraph);

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
                    Console.WriteLine($"PDF saved to '{outputPath}'. (Saved on non‑Windows platform – ensure libgdiplus is installed.)");
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
