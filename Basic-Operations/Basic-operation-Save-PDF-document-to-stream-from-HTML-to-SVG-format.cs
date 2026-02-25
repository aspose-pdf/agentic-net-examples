using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";
        const string svgFilePath = "output.svg";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Convert HTML to SVG and save directly to a file
        try
        {
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                SvgSaveOptions svgOptions = new SvgSaveOptions();
                doc.Save(svgFilePath, svgOptions);
            }

            Console.WriteLine($"HTML successfully converted to SVG: {svgFilePath}");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion requires GDI+ (Windows only)
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }

        // Example: save the SVG output to a MemoryStream instead of a file
        try
        {
            using (Document doc = new Document(htmlPath, new HtmlLoadOptions()))
            {
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                using (MemoryStream svgStream = new MemoryStream())
                {
                    doc.Save(svgStream, svgOptions);
                    svgStream.Position = 0; // rewind for reading

                    // Optionally write the stream to a file
                    using (FileStream file = File.Create("output_from_stream.svg"))
                    {
                        svgStream.CopyTo(file);
                    }
                }
            }

            Console.WriteLine("SVG saved to stream and written to file successfully.");
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML to SVG conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during stream save: {ex.Message}");
        }
    }
}