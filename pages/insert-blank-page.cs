using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class InsertBlankPageExample
{
    public static void Main()
    {
        const string outputPath = "output.pdf";
        using (Document doc = new Document())
        {
            // Create three pages with sample text
            Page page1 = doc.Pages.Add();
            page1.Paragraphs.Add(new TextFragment("Page 1"));
            Page page2 = doc.Pages.Add();
            page2.Paragraphs.Add(new TextFragment("Page 2"));
            Page page3 = doc.Pages.Add();
            page3.Paragraphs.Add(new TextFragment("Page 3"));

            // Insert a blank page at position 3 (1‑based index)
            Page blankPage = doc.Pages.Insert(3);
            // Optionally add a label to the inserted page
            blankPage.Paragraphs.Add(new TextFragment("Inserted Blank Page"));

            // Save the updated document – guard against missing GDI+ on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
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