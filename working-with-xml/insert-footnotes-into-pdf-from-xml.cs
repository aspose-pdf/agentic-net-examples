using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string footnotesXmlPath = "footnotes.xml";
        const string outputPdfPath  = "output_with_footnotes.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(footnotesXmlPath))
        {
            Console.Error.WriteLine($"Footnotes XML not found: {footnotesXmlPath}");
            return;
        }

        // Load the XML that contains footnote definitions.
        // Expected format:
        // <Footnotes>
        //   <Footnote id="1">First footnote text.</Footnote>
        //   <Footnote id="2">Second footnote text.</Footnote>
        //   ...
        // </Footnotes>
        XDocument xmlDoc = XDocument.Load(footnotesXmlPath);
        var footnoteElements = xmlDoc.Root?.Elements("Footnote");

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // For demonstration, attach the first footnote to each page.
                // In a real scenario you would match footnote references to page content.
                XElement footnoteXml = footnoteElements?.FirstOrDefault();
                if (footnoteXml == null) continue;

                string footnoteText = footnoteXml.Value.Trim();

                // Create a Note object (represents a footnote structure element).
                Note note = new Note(footnoteText);

                // Create a TextFragment that will hold the footnote reference.
                // Position it near the bottom of the page.
                TextFragment tf = new TextFragment(" "); // placeholder text
                tf.Position = new Position(50, 50); // X=50, Y=50 (adjust as needed)
                tf.FootNote = note; // Associate the footnote with the fragment.

                // Optionally style the placeholder (e.g., superscript number).
                tf.TextState.FontSize = 8;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the fragment to the page.
                page.Paragraphs.Add(tf);
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with footnotes saved to '{outputPdfPath}'.");
    }
}