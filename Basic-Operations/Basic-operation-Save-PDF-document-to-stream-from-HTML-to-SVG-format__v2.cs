using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions classes are in this namespace

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"File not found: {htmlPath}");
            return;
        }

        try
        {
            // Load the HTML file into a Document (requires GDI+ on Windows)
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document doc = new Document(htmlPath, loadOptions))
            {
                // Configure SVG save options (default constructor is sufficient here)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the converted SVG into a memory stream
                using (MemoryStream stream = new MemoryStream())
                {
                    doc.Save(stream, svgOptions);
                    stream.Position = 0; // Reset for any subsequent reading

                    // Example: write the stream contents to a file (optional)
                    File.WriteAllBytes("output.svg", stream.ToArray());

                    Console.WriteLine("HTML successfully converted to SVG and saved to stream.");
                }
            }
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑SVG conversion uses GDI+, which is Windows‑only
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}