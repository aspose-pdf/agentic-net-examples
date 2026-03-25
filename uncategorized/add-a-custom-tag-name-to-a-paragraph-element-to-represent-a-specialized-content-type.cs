using System;
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
            // Access tagged content
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Custom Tag Example");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a paragraph element and assign a custom tag
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetTag("MySpecialParagraph"); // custom tag name
            para.SetText("This paragraph uses a custom tag to represent specialized content.");

            // Attach the paragraph to the root
            root.AppendChild(para);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
