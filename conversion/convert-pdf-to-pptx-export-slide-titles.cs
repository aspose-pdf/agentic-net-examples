using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        // Input PDF, intermediate PPTX, and output CSV paths.
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string csvPath = "slide_titles.csv";

        // Ensure a source PDF exists – create a simple one if missing.
        if (!File.Exists(pdfPath))
        {
            CreateSamplePdf(pdfPath);
        }

        // ---------- Convert PDF to PPTX ----------
        // Use Aspose.Pdf Document and PptxSaveOptions (no extra namespaces required).
        using (Document pdfDoc = new Document(pdfPath))
        {
            PptxSaveOptions saveOpts = new PptxSaveOptions(); // default options
            pdfDoc.Save(pptxPath, saveOpts);                 // saves as PPTX
        }

        // ---------- Extract slide titles from the generated PPTX ----------
        // PPTX files are ZIP archives containing XML parts. Slide titles are stored in
        // ppt/slides/slideX.xml inside a shape with a placeholder of type "title".
        using (ZipArchive archive = ZipFile.OpenRead(pptxPath))
        using (StreamWriter csvWriter = new StreamWriter(csvPath))
        {
            // Write CSV header.
            csvWriter.WriteLine("SlideNumber,Title");

            // Gather slide XML entries in order (slide1.xml, slide2.xml, ...).
            var slideEntries = archive.Entries
                .Where(e => e.FullName.StartsWith("ppt/slides/slide", StringComparison.OrdinalIgnoreCase)
                         && e.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                .OrderBy(e => e.FullName);

            int slideNumber = 1;
            foreach (var entry in slideEntries)
            {
                using (Stream entryStream = entry.Open())
                {
                    // Load the slide XML.
                    XDocument slideXml = XDocument.Load(entryStream);

                    // XML namespaces used in PPTX parts.
                    XNamespace p = "http://schemas.openxmlformats.org/presentationml/2006/main";
                    XNamespace a = "http://schemas.openxmlformats.org/drawingml/2006/main";

                    // Find the first text element (<a:t>) inside a shape (<p:sp>) that
                    // contains a placeholder (<p:ph>) with type="title".
                    string? title = slideXml
                        .Descendants(p + "sp")
                        .Where(sp => sp.Descendants(p + "ph")
                                      .Any(ph => (string?)ph.Attribute("type") == "title"))
                        .Descendants(a + "t")
                        .Select(t => (string?)t)
                        .FirstOrDefault();

                    // Ensure title is not null for CSV output.
                    title ??= string.Empty;

                    // Escape double quotes for CSV compliance.
                    string escapedTitle = title.Replace("\"", "\"\"");

                    // Write slide number and title to CSV.
                    csvWriter.WriteLine($"{slideNumber},\"{escapedTitle}\"");
                }

                slideNumber++;
            }
        }

        Console.WriteLine($"Conversion complete. PPTX saved to '{pptxPath}'. Slide titles exported to '{csvPath}'.");
    }

    private static void CreateSamplePdf(string path)
    {
        // Create a minimal PDF with a single page containing a title.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            // Add a simple text fragment that will become the slide title after conversion.
            page.Paragraphs.Add(new TextFragment("Sample Presentation Title"));
            doc.Save(path);
        }
    }
}
