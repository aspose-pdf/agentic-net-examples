using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF file path
        string outputPath = "output.pdf";

        // Font name to use when a required font is missing (Windows only)
        string defaultFontName = "Arial";

        try
        {
            // Create a new PDF document
            using (Document doc = new Document())
            {
                // Add a page to the document
                Page page = doc.Pages.Add();

                // Add a simple text fragment (no explicit font set)
                TextFragment text = new TextFragment("Sample text using default substitution font.");
                text.TextState.FontSize = 14;
                page.Paragraphs.Add(text);

                // Configure save options. On non‑Windows platforms Aspose.Pdf may try to initialise GDI+ when a default font name is supplied, which results in a TypeInitializationException if libgdiplus is not present.
                // Therefore we set the DefaultFontName only when running on Windows.
                PdfSaveOptions saveOptions = new PdfSaveOptions();
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    saveOptions.DefaultFontName = defaultFontName;
                }
                else
                {
                    Console.WriteLine("Running on a non‑Windows OS – skipping DefaultFontName to avoid GDI+ initialization errors.");
                }

                // Save the PDF using the configured options
                doc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'{(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? $" with default font '{defaultFontName}'" : string.Empty)}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while creating PDF: {ex.Message}");
        }
    }
}
