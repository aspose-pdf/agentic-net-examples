using System;
using System.IO;
using System.Text;
using System.IO.Compression;
using System.Xml.Linq;
using Aspose.Pdf;

class PdfToPptxAndExtractTitles
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string csvPath = "slide_titles.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Convert PDF to PPTX using Aspose.Pdf core API
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // Extract slide titles from the generated PPTX without using Aspose.Slides
        using (var csvWriter = new StreamWriter(csvPath, false, Encoding.UTF8))
        {
            csvWriter.WriteLine("SlideNumber,Title"); // CSV header

            using (var archive = ZipFile.OpenRead(pptxPath))
            {
                // PPTX stores slides as ppt/slides/slide1.xml, slide2.xml, ...
                var slideEntries = archive.Entries;
                int slideIndex = 0;
                foreach (var entry in slideEntries)
                {
                    if (!entry.FullName.StartsWith("ppt/slides/slide", StringComparison.OrdinalIgnoreCase) ||
                        !entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        continue;

                    slideIndex++;
                    string titleText = string.Empty;

                    using (var stream = entry.Open())
                    {
                        XDocument slideDoc = XDocument.Load(stream);
                        XNamespace p = "http://schemas.openxmlformats.org/presentationml/2006/main";
                        XNamespace a = "http://schemas.openxmlformats.org/drawingml/2006/main";

                        // Find shape (<p:sp>) that contains a placeholder (<p:ph>) with type="title"
                        var titleShape = slideDoc.Root
                            .Descendants(p + "sp")
                            .FirstOrDefault(sp => sp.Element(p + "ph")?.Attribute("type")?.Value == "title");

                        if (titleShape != null)
                        {
                            // Extract all text (<a:t>) inside the shape
                            var textRuns = titleShape.Descendants(a + "t");
                            titleText = string.Join(" ", textRuns.Select(tr => tr.Value));
                        }
                    }

                    // Escape double quotes for CSV compliance
                    titleText = titleText.Replace("\"", "\"\"");
                    csvWriter.WriteLine($"{slideIndex},\"{titleText}\"");
                }
            }
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{pptxPath}'. Slide titles exported to '{csvPath}'.");
    }
}
