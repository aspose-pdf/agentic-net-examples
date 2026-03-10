using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        try
        {
            // Create a new PDF document and ensure it is disposed properly
            using (Document doc = new Document())
            {
                // Add a blank page to the document
                Page page = doc.Pages.Add();

                // Create a simple text fragment and add it to the page
                TextFragment text = new TextFragment("Hello, Aspose.Pdf!");
                page.Paragraphs.Add(text);

                // On macOS (or other non‑Windows platforms) Aspose.Pdf features that rely on GDI+ (e.g., Graph)
                // can throw a TypeInitializationException if libgdiplus is not installed.
                // This sample does not use such features, but we keep the guard for future extensions.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Console.WriteLine("Running on macOS – saving PDF without GDI+ dependent features.");
                }

                // Persist the document to the specified file
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating PDF: {ex.Message}");
        }
    }
}
