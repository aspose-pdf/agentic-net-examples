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
        const string outputPath = "custom_tagged.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document language and title
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with custom paragraph tag");

            // Get the root element of the logical structure
            StructureElement root = tagged.RootElement;

            // Create a paragraph element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Assign a custom tag name to represent specialized content
            // Use SetTag (the correct API) to set a custom tag name.
            paragraph.SetTag("SpecialParagraph");

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph uses a custom tag for specialized content.");

            // Append the paragraph to the root element
            root.AppendChild(paragraph);

            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
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
