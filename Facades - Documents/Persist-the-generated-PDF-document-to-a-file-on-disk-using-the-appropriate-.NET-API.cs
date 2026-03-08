using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path where the PDF will be saved
        const string outputPath = "output.pdf";

        try
        {
            // Create a new PDF document and ensure it is disposed properly
            using (Document doc = new Document())
            {
                // Add a blank page to the document
                Page page = doc.Pages.Add();

                // Add a simple text fragment to the page
                TextFragment text = new TextFragment("Hello, Aspose.Pdf!");
                page.Paragraphs.Add(text);

                // On macOS the Aspose.Pdf engine may try to initialise GDI+ (System.Drawing).
                // If libgdiplus is not present this results in a TypeInitializationException.
                // We avoid any GDI+‑dependent rendering and simply save the PDF.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Console.WriteLine("Running on macOS – libgdiplus is required for GDI+ features. " +
                                      "Saving PDF without GDI+ dependent content.");
                }

                // Persist the PDF to the specified file
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while creating PDF: {ex.Message}");
        }
    }
}
