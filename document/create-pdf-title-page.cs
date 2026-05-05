using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "title_page.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page will be the title page)
            Page page = doc.Pages.Add();

            // Create a text fragment for the title
            TextFragment title = new TextFragment("My Custom Title");

            // Set custom font, size and color via the TextState object
            title.TextState.Font = FontRepository.FindFont("Helvetica");
            title.TextState.FontSize = 36;
            // Fully qualify the Color type to avoid ambiguity with System.Drawing.Color
            title.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the title near the top center of the page
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            title.Position = new Position(pageWidth / 2, pageHeight - 100);
            title.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Add the title fragment to the page
            page.Paragraphs.Add(title);

            // Save the PDF document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        // Fully qualify System.IO.Path to avoid conflict with Aspose.Pdf.Drawing.Path
        Console.WriteLine($"PDF with title page saved to '{Path.GetFullPath(outputPath)}'.");
    }

    // Helper method to walk the inner‑exception chain and detect a missing native library
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
