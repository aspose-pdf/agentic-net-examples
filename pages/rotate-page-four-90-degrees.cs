using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add four pages (evaluation mode limit is 4 pages)
            for (int i = 1; i <= 4; i++)
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment("Page " + i);
                page.Paragraphs.Add(tf);
            }

            // Rotate the fourth page 90 degrees clockwise using the Rotate property
            Page fourthPage = doc.Pages[4];
            fourthPage.Rotate = Rotation.on90; // correct enum value with 'on' prefix

            // Save the modified PDF – guard against missing libgdiplus on non‑Windows platforms
            string outputPath = "rotated_page_four.pdf";

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
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }

            Console.WriteLine($"Page 4 rotated. {(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? $"Saved to {outputPath}" : "Save skipped on this platform.")}");
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
