using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document (or load an existing one)
        using (Document document = new Document())
        {
            // Add a few pages (evaluation mode allows a maximum of 4 pages)
            for (int i = 1; i <= 4; i++)
            {
                Page page = document.Pages.Add();
                TextFragment tf = new TextFragment($"Page {i}");
                page.Paragraphs.Add(tf);
            }

            // JavaScript that navigates to page 10 (zero‑based index 9)
            string js = "this.pageNum = 9;";
            document.OpenAction = new JavascriptAction(js);

            string outputPath = "output.pdf";

            // Guard Document.Save on platforms that lack GDI+ (libgdiplus)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    document.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF not saved.");
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