using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Devices;

#nullable enable

class Program
{
    static void Main()
    {
        const string xmlFileName = "sample.xml";
        // Resolve to an absolute path – Aspose sometimes treats the string as a URI.
        string xmlPath = Path.GetFullPath(xmlFileName);

        // ---------------------------------------------------------------------
        // Step 1 – create a PDF and save it as XML
        // ---------------------------------------------------------------------
        using (var doc = new Document())
        {
            var page = doc.Pages.Add();
            var text = new TextFragment("Hello from XML conversion!");
            page.Paragraphs.Add(text);

            var xmlOptions = new XmlSaveOptions();

            // Save the document as XML. The try/catch is kept only to surface a clear
            // message if the underlying GDI+ dependency is missing.
            try
            {
                doc.Save(xmlPath, xmlOptions);
                Console.WriteLine($"XML representation saved to '{xmlPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("GDI+ (libgdiplus) is not available – XML saving is skipped.");
                return; // No XML → cannot continue.
            }
        }

        // ---------------------------------------------------------------------
        // Step 2 – load the PDF from the generated XML
        // ---------------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"The XML file '{xmlPath}' was not created. Exiting.");
            return;
        }

        using (var pdfFromXml = new Document())
        {
            pdfFromXml.BindXml(xmlPath);

            // -----------------------------------------------------------------
            // Step 3 – export each page as a PNG image
            // -----------------------------------------------------------------
            for (int i = 1; i <= pdfFromXml.Pages.Count; i++)
            {
                string imagePath = $"page_{i}.png";

                try
                {
                    using (var imageStream = new FileStream(imagePath, FileMode.Create))
                    {
                        var pngDevice = new PngDevice(new Resolution(300));
                        pngDevice.Process(pdfFromXml.Pages[i], imageStream);
                    }
                    Console.WriteLine($"Page {i} saved to '{imagePath}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine($"Cannot render page {i} – GDI+ (libgdiplus) is missing on this platform.");
                }
            }
        }
    }

    // Helper that walks the InnerException chain looking for a DllNotFoundException.
    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception current = ex;
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}
