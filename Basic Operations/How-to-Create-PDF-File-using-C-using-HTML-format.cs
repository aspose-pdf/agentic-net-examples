using System;
using System.IO;
using Aspose.Pdf;          // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // Define the folder that contains the source HTML file.
        // Replace "YOUR_DATA_DIRECTORY" with the actual path on your machine.
        // -----------------------------------------------------------------
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input HTML file and output PDF file paths.
        string htmlPath = Path.Combine(dataDir, "HTML-to-PDF.html");
        string pdfPath  = Path.Combine(dataDir, "HTML-to-PDF.pdf");

        // Verify that the HTML source file exists before attempting conversion.
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"Error: HTML source file not found at '{htmlPath}'.");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Load the HTML file into a PDF Document.
            // HtmlLoadOptions can be instantiated with an empty constructor;
            // optionally you could pass the base path (dataDir) to resolve relative resources.
            // -----------------------------------------------------------------
            HtmlLoadOptions loadOptions = new HtmlLoadOptions(); // or new HtmlLoadOptions(dataDir);

            // The Document constructor with (string filename, LoadOptions options) creates a PDF
            // representation of the supplied HTML.
            using (Document pdfDocument = new Document(htmlPath, loadOptions))
            {
                // Save the generated PDF to the desired location.
                pdfDocument.Save(pdfPath);
            }

            Console.WriteLine($"PDF successfully created at: {pdfPath}");
        }
        catch (TypeInitializationException tie)
        {
            // HTML-to-PDF conversion relies on GDI+ and is Windows‑only.
            // If the code runs on macOS/Linux, this exception may be thrown.
            Console.Error.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
            Console.Error.WriteLine($"Details: {tie.Message}");
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}