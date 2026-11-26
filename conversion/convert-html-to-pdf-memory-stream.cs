using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";   // source HTML file
        const string outputPdf = "output.pdf"; // final PDF file on disk

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML document
            using (Document doc = new Document(htmlPath))
            {
                // Save PDF into a memory stream (default format is PDF)
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    doc.Save(pdfStream);

                    // Persist the PDF bytes to a file
                    File.WriteAllBytes(outputPdf, pdfStream.ToArray());
                }
            }

            Console.WriteLine($"HTML converted to PDF and saved as '{outputPdf}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML to PDF conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
