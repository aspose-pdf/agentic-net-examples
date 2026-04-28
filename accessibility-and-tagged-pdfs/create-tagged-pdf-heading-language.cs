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

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set the document language (optional but demonstrates usage)
            tagged.SetLanguage("en-US");
            // Set a title for the PDF (optional)
            tagged.SetTitle("Document with Heading");

            // Obtain the root structure element (no cast required)
            StructureElement root = tagged.RootElement;

            // Create a level‑1 heading element
            HeaderElement heading = tagged.CreateHeaderElement(1);
            // Define the visible text of the heading
            heading.SetText("Chapter 1: Introduction");
            // Assign a language attribute to the heading element
            heading.Language = "en-US";

            // Attach the heading to the document's structure tree
            root.AppendChild(heading);

            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
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
                                      "The PDF could not be saved.");
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
