using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlPath = "input.html";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlPath}");
            return;
        }

        // Load HTML from a file stream and convert it to SVG written into a memory stream
        using (FileStream htmlStream = File.OpenRead(htmlPath))
        using (MemoryStream svgStream = new MemoryStream())
        {
            // Convert HTML to SVG using the static Convert method with explicit load and save options
            Document.Convert(htmlStream, new HtmlLoadOptions(), svgStream, new SvgSaveOptions());

            // Reset the stream position if further reading is required
            svgStream.Position = 0;

            // Optional: write the SVG data to a file for verification
            File.WriteAllBytes("output.svg", svgStream.ToArray());

            Console.WriteLine("HTML successfully converted to SVG and saved to output.svg");
        }
    }
}