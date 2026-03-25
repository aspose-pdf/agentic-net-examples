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
        const string outputPath = "heading_language.pdf";

        using (Document doc = new Document())
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document‑wide language and title
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample PDF with Heading");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a level‑1 heading element
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Chapter 1: Introduction");
            // Assign a language attribute to the heading (optional override)
            heading.Language = "en-US";

            // Attach the heading to the root element
            root.AppendChild(heading);

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
                                      "The PDF was generated in memory but could not be saved to disk.");
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
