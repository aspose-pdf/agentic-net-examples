using System;
using System.IO;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string xmlPath = "pagesize.xml";
        // Default to A4 size (points) if the XML is missing or malformed
        double width = PageSize.A4.Width;
        double height = PageSize.A4.Height;

        if (File.Exists(xmlPath))
        {
            try
            {
                XDocument xDoc = XDocument.Load(xmlPath);
                XElement wElem = xDoc.Root.Element("Width");
                XElement hElem = xDoc.Root.Element("Height");

                if (wElem != null && double.TryParse(wElem.Value, out double w))
                    width = w;
                if (hElem != null && double.TryParse(hElem.Value, out double h))
                    height = h;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading '{xmlPath}': {ex.Message}. Using default page size.");
            }
        }
        else
        {
            Console.WriteLine($"File '{xmlPath}' not found. Using default A4 page size.");
        }

        // Create a new PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a blank page
            Page page = pdfDocument.Pages.Add();

            // Set custom page dimensions via PageInfo (Aspose.Pdf requirement)
            page.PageInfo.Width = width;
            page.PageInfo.Height = height;

            // Add a simple text fragment to demonstrate the page size
            TextFragment text = new TextFragment($"Page size: {width} x {height}");
            page.Paragraphs.Add(text);

            // Save the resulting PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            const string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                pdfDocument.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    pdfDocument.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform)." );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF was not saved, but the program ran without crashing.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain to detect a missing native GDI+ library
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
