using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputFolder = "HtmlOutput";
        const string htmlBaseName = "output.html";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Ensure the folder for external resources exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Create a separate HTML file for each PDF page
                    SplitIntoPages = true
                    // In this Aspose.Pdf version SVG handling, compression and
                    // custom output folder are managed automatically. No explicit
                    // properties are required.
                };

                // The base HTML file name; Aspose.Pdf will append page numbers
                string htmlPath = Path.Combine(outputFolder, htmlBaseName);

                // Perform the conversion
                pdfDoc.Save(htmlPath, htmlOpts);

                Console.WriteLine($"PDF successfully converted to split HTML pages in folder: {outputFolder}");
            }
        }
        // HTML conversion relies on GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}
