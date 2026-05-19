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
        const string outputPath = "tagged_heading.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;

            // Set document‑level language and title
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Heading");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a level‑1 heading (HeaderElement)
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Sample Heading");

            // Assign a language attribute to the heading element
            heading.Language = "en-US";

            // Attach the heading to the root element
            root.AppendChild(heading);

            // Save the PDF (guard against missing GDI+ on non‑Windows platforms)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ not available; PDF saved without graphics.");
                }
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException) return true;
            ex = ex.InnerException;
        }
        return false;
    }
}