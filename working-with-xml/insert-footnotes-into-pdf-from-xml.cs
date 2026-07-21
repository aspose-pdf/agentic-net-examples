using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string footnotesXml = "footnotes.xml";
        const string outputPdf = "output_with_footnotes.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(footnotesXml))
        {
            Console.Error.WriteLine("Missing input PDF or footnotes XML file.");
            return;
        }

        // Load footnote definitions from XML.
        // Expected XML format:
        // <Footnotes>
        //   <Footnote id="1">First footnote text.</Footnote>
        //   <Footnote id="2">Second footnote text.</Footnote>
        //   ...
        // </Footnotes>
        var footnoteMap = XDocument.Load(footnotesXml)
            .Root?
            .Elements("Footnote")
            .ToDictionary(
                e => (string)e.Attribute("id"),
                e => (string)e.Value);

        if (footnoteMap == null || footnoteMap.Count == 0)
        {
            Console.Error.WriteLine("No footnotes found in the XML.");
            return;
        }

        // Open the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through each page (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Insert footnotes at the bottom of the page.
                // Adjust Y coordinate for each footnote to avoid overlap.
                int footnoteCounter = 0;
                foreach (var kvp in footnoteMap)
                {
                    footnoteCounter++;

                    // Create a Note object containing the footnote text.
                    Note note = new Note(kvp.Value);

                    // Create a TextFragment; the visible text can be a placeholder
                    // because the actual footnote content is stored in the Note.
                    TextFragment tf = new TextFragment(" "); // placeholder
                    tf.FootNote = note; // associate the footnote

                    // Position the footnote near the bottom left of the page.
                    // 50 points from the left margin, and a vertical offset
                    // that increases with each footnote.
                    double x = 50;
                    double y = 50 + (footnoteCounter - 1) * 15; // 15 pt spacing
                    tf.Position = new Position(x, y);

                    // Add the fragment to the page.
                    page.Paragraphs.Add(tf);
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with footnotes saved to '{outputPdf}'.");
    }
}