using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        // Create a new empty PDF document
        Document doc = new Document();

        // Access the tagged‑content API
        ITaggedContent tagged = doc.TaggedContent;

        // (Optional) Set language and title for the document
        tagged.SetLanguage("en-US");
        tagged.SetTitle("Heading Example");

        // Get the root structure element of the tagged tree
        StructureElement root = tagged.RootElement;

        // Create a Level 1 heading element
        HeaderElement heading = tagged.CreateHeaderElement(1);
        heading.SetText("Level 1 Heading");

        // Attach the heading to the root element
        root.AppendChild(heading);

        string outputPath = "heading.pdf";

        // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF may not be fully rendered.");
            }
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
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