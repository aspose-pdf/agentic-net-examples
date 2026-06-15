using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";
        const string outputCsvPath = "slide_titles.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // ---------- Convert PDF to PPTX ----------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Use the correct save options for PPTX conversion
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(outputPptxPath, pptxOptions);
        }

        if (!File.Exists(outputPptxPath))
        {
            Console.Error.WriteLine($"Failed to create PPTX file: {outputPptxPath}");
            return;
        }

        // ---------- Extract slide titles from the generated PPTX ----------
        var slideTitles = new List<(int SlideNumber, string Title)>();

        using (FileStream pptxStream = new FileStream(outputPptxPath, FileMode.Open, FileAccess.Read))
        using (ZipArchive zip = new ZipArchive(pptxStream, ZipArchiveMode.Read))
        {
            // PPTX stores slides under ppt/slides/slideX.xml
            var slideEntries = zip.Entries
                .Where(e => e.FullName.StartsWith("ppt/slides/slide", StringComparison.OrdinalIgnoreCase) &&
                            e.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                .OrderBy(e => e.FullName); // Ensure natural order

            foreach (var entry in slideEntries)
            {
                // Extract slide number from the file name (e.g., slide1.xml -> 1)
                string fileName = Path.GetFileNameWithoutExtension(entry.Name);
                int slideNumber = 0;
                if (fileName.Length > 5 && int.TryParse(fileName.Substring(5), out int num))
                    slideNumber = num;

                string titleText = string.Empty;

                using (Stream entryStream = entry.Open())
                {
                    XDocument slideDoc = XDocument.Load(entryStream);
                    XNamespace p = "http://schemas.openxmlformats.org/presentationml/2006/main";
                    XNamespace a = "http://schemas.openxmlformats.org/drawingml/2006/main";

                    // Find a shape (<p:sp>) that contains a placeholder of type "title"
                    var titleShape = slideDoc.Descendants(p + "sp")
                        .FirstOrDefault(sp => sp.Descendants(p + "ph")
                            .Any(ph => (string)ph.Attribute("type") == "title"));

                    if (titleShape != null)
                    {
                        // Extract all text runs (<a:t>) within the shape
                        var textRuns = titleShape.Descendants(a + "t");
                        titleText = string.Concat(textRuns.Select(t => (string)t));
                    }
                }

                slideTitles.Add((slideNumber, titleText));
            }
        }

        // ---------- Write titles to CSV ----------
        using (StreamWriter writer = new StreamWriter(outputCsvPath, false, Encoding.UTF8))
        {
            writer.WriteLine("SlideNumber,Title");
            foreach (var (SlideNumber, Title) in slideTitles.OrderBy(st => st.SlideNumber))
            {
                // Escape double quotes in title
                string escapedTitle = Title?.Replace("\"", "\"\"");
                writer.WriteLine($"{SlideNumber},\"{escapedTitle}\"");
            }
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{outputPptxPath}'.");
        Console.WriteLine($"Slide titles extracted to CSV at '{outputCsvPath}'.");
    }
}