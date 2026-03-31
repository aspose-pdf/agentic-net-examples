using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Retrieve the predefined A4 size (points are used internally)
            PageSize a4Size = PageSize.A4;

            // Set the page to landscape orientation by swapping width and height
            double landscapeWidth = a4Size.Height;   // 297 mm in points
            double landscapeHeight = a4Size.Width;   // 210 mm in points
            page.SetPageSize(landscapeWidth, landscapeHeight);

            // Add a simple text fragment to verify the page size
            TextFragment fragment = new TextFragment("A4 Landscape Page");
            fragment.Position = new Position(50, landscapeHeight - 50);
            page.Paragraphs.Add(fragment);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was created without GDI+ dependent features.");
                }
            }
        }

        Console.WriteLine($"PDF with A4 landscape page size created as '{outputPath}'.");
    }

    // Helper to walk the inner‑exception chain and detect a missing native GDI+ library
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