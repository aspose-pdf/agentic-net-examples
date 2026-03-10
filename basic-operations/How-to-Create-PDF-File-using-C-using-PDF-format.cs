using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Enable System.Drawing support on non‑Windows platforms (requires libgdiplus).
        // This switch must be set **before** any Aspose.Pdf types are touched.
        AppContext.SetSwitch("System.Drawing.EnableUnixSupport", true);

        // Output PDF file path
        string outputPath = "created.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        try
        {
            // Create a new PDF document inside a using block (deterministic disposal)
            using (Document doc = new Document())
            {
                // Add a blank page (pages are 1‑based)
                Page page = doc.Pages.Add();

                // Create a text fragment and set its position
                TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");
                tf.Position = new Position(100, 700); // X=100, Y=700 (from bottom‑left)

                // Add the text fragment to the page
                page.Paragraphs.Add(tf);

                // Save the document as PDF (no SaveOptions needed for PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF created at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Provide a clear message when the required native GDI+ library (libgdiplus) is missing.
            Console.Error.WriteLine("Failed to create PDF. On Linux/macOS you need the native GDI+ library (libgdiplus).\n" +
                                "Install it via your package manager, e.g., 'sudo apt-get install -y libgdiplus' or 'brew install mono-libgdiplus'.\n" +
                                $"Exception: {ex.Message}");
        }
    }
}
