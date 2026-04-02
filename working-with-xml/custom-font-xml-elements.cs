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
        // Create a simple XML file with title and body elements
        string xmlContent = "<?xml version='1.0' encoding='utf-8'?><document><title>Sample Title</title><body>This is the body text of the document.</body></document>";
        string xmlPath = "sample.xml";
        File.WriteAllText(xmlPath, xmlContent);

        // Load the custom XML using LINQ to XML
        XDocument xdoc = XDocument.Load(xmlPath);
        string title = xdoc.Root.Element("title")?.Value ?? string.Empty;
        string body = xdoc.Root.Element("body")?.Value ?? string.Empty;

        // Create a new PDF document
        Document pdfDocument = new Document();
        Page page = pdfDocument.Pages.Add();

        // Add the title with a custom font and larger size
        TextFragment titleFragment = new TextFragment(title);
        titleFragment.TextState.Font = FontRepository.FindFont("Arial");
        titleFragment.TextState.FontSize = 20f; // float literal as required by the API
        titleFragment.TextState.FontStyle = FontStyles.Bold;
        page.Paragraphs.Add(titleFragment);

        // Add a blank line for spacing
        page.Paragraphs.Add(new TextFragment("\n"));

        // Add the body text with a regular font size
        TextFragment bodyFragment = new TextFragment(body);
        bodyFragment.TextState.Font = FontRepository.FindFont("Arial");
        bodyFragment.TextState.FontSize = 12f;
        page.Paragraphs.Add(bodyFragment);

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
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
            {
                Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
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
