using System;
using System.IO;
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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(footnotesXml))
        {
            Console.Error.WriteLine($"Footnotes XML not found: {footnotesXml}");
            return;
        }

        // Load footnote definitions from XML.
        // Expected format:
        // <Footnotes>
        //   <Footnote Page="1">First footnote text.</Footnote>
        //   <Footnote Page="2">Second footnote text.</Footnote>
        // </Footnotes>
        XDocument xmlDoc = XDocument.Load(footnotesXml);
        var footnoteElements = xmlDoc.Root?.Elements("Footnote");

        // Open the PDF inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdf))
        {
            if (footnoteElements != null)
            {
                foreach (var fn in footnoteElements)
                {
                    // Parse the target page number (1‑based indexing).
                    if (!int.TryParse(fn.Attribute("Page")?.Value, out int pageNumber))
                        continue;

                    // Validate page range.
                    if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
                        continue;

                    string footnoteText = fn.Value.Trim();
                    if (string.IsNullOrEmpty(footnoteText))
                        continue;

                    // Create a TextFragment for the footnote.
                    TextFragment tf = new TextFragment(footnoteText);
                    tf.TextState.FontSize = 8; // smaller font for footnotes
                    tf.TextState.Font = FontRepository.FindFont("Arial");
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                    // Determine absolute position near the bottom of the page.
                    Page page = pdfDoc.Pages[pageNumber];
                    double pageWidth = page.PageInfo.Width;
                    double marginBottom = 20; // points from bottom edge
                    double lineHeight = 12;   // approximate height for the footnote line

                    // Set the position (lower‑left corner) using Aspose.Pdf.Text.Position.
                    tf.Position = new Position(0, marginBottom);

                    // Optionally, constrain the width by setting the rectangle (not required for simple footnotes).
                    // Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, marginBottom, pageWidth, marginBottom + lineHeight);
                    // tf.Position = new Position(rect.LLX, rect.LLY);

                    // Add the fragment to the page. Setting Position makes it absolute.
                    page.Paragraphs.Add(tf);
                }
            }

            // Save the modified PDF (PDF format, no SaveOptions needed).
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Footnotes inserted and saved to '{outputPdf}'.");
    }
}