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
        const string outputPath = "customTagParagraph.pdf";

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Obtain the tagged‑content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Custom Tag Example");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph structure element
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Assign a custom tag name to represent a specialized content type
            paragraph.SetTag("MySpecialParagraph");

            // Set the visible text of the paragraph
            paragraph.SetText("This paragraph has a custom tag for specialized content.");

            // Attach the paragraph to the root of the structure tree
            root.AppendChild(paragraph);

            // Save the resulting PDF – guard against missing GDI+ on non‑Windows platforms
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
