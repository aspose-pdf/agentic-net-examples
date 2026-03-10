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

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document (1‑based indexing)
            Page page = doc.Pages.Add();

            // Add a simple text fragment to the page
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");

            // Use a built‑in PDF font that does not require GDI+ (avoids libgdiplus issues on macOS/Linux)
            // On Windows we can safely use any installed font; on other platforms we stick to a standard PDF font.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
            }
            else
            {
                tf.TextState.Font = FontRepository.FindFont("Arial");
            }

            page.Paragraphs.Add(tf);

            // Persist the PDF content to a file using standard PDF binary encoding
            try
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                // If libgdiplus is missing on macOS/Linux, the exception will be caught here.
                Console.WriteLine($"Failed to save PDF: {ex.Message}");
            }
        }
    }
}
