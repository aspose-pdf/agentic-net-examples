using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf; // Core Aspose.Pdf namespace provides Document and SaveFormat

class Program
{
    static void Main()
    {
        // Input PDF and intermediate PPTX paths
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string csvPath = "slide_titles.csv";

        // Validate input PDF existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        // Load PDF document (using Aspose.Pdf) and save directly as PPTX
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // ---------- Extract slide titles from the generated PPTX ----------
        // The PPTX file is a ZIP archive containing XML parts. We read each slide XML
        // and look for a placeholder of type "title". The text inside <a:t> elements
        // represents the title.
        using (var writer = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("SlideNumber,Title");

            using (ZipArchive archive = ZipFile.OpenRead(pptxPath))
            {
                // Slides are stored under "ppt/slides/slideX.xml" where X starts at 1
                int slideIndex = 1;
                while (true)
                {
                    string entryName = $"ppt/slides/slide{slideIndex}.xml";
                    var entry = archive.GetEntry(entryName);
                    if (entry == null)
                        break; // No more slides

                    string titleText = ExtractTitleFromSlide(entry);
                    // Escape double quotes for CSV compliance
                    string escapedTitle = $"\"{titleText.Replace("\"", "\"\"")}\"";
                    writer.WriteLine($"{slideIndex},{escapedTitle}");
                    slideIndex++;
                }
            }
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{pptxPath}'. Slide titles exported to '{csvPath}'.");
    }

    /// <summary>
    /// Reads a slide XML entry from the PPTX package and returns the text of the title placeholder.
    /// If no title placeholder is found, returns an empty string.
    /// </summary>
    private static string ExtractTitleFromSlide(ZipArchiveEntry slideEntry)
    {
        using (var stream = slideEntry.Open())
        {
            XDocument doc = XDocument.Load(stream);
            // Namespace declarations used in PPTX XML parts
            XNamespace p = "http://schemas.openxmlformats.org/presentationml/2006/main";
            XNamespace a = "http://schemas.openxmlformats.org/drawingml/2006/main";

            // Find a shape that contains a placeholder of type "title"
            var titleShape = doc.Root
                .Descendants(p + "sp")
                .FirstOrDefault(sp =>
                    sp.Descendants(p + "ph")
                      .Any(ph => (string)ph.Attribute("type") == "title"));

            if (titleShape == null)
                return string.Empty;

            // Collect all text runs (<a:t>) inside the shape's text body
            var texts = titleShape
                .Descendants(a + "t")
                .Select(t => (string)t)
                .ToArray();

            return string.Join(" ", texts).Trim();
        }
    }
}
