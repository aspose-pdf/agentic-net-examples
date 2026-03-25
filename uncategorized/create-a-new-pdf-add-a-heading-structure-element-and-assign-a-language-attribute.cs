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
        const string outputPath = "heading_tagged.pdf";

        using (Document doc = new Document())
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document‑level language and title
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Heading");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a level‑1 heading element
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Chapter 1: Introduction");
            // Assign a language attribute to the heading element (use property, not SetLanguage method)
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
    }

    private static bool ContainsDllNotFound(Exception ex)
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
