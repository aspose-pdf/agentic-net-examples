using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xmlPath = "footnotes.xml";
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML not found: {xmlPath}");
            return;
        }

        // Load footnote definitions from XML.
        // Expected format:
        // <footnotes>
        //   <footnote page="1" number="1">First footnote text.</footnote>
        //   <footnote page="2" number="2">Second footnote text.</footnote>
        // </footnotes>
        XDocument xDoc = XDocument.Load(xmlPath);
        var footnoteElements = xDoc.Root?.Elements("footnote");

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(pdfPath))
        {
            if (footnoteElements != null)
            {
                foreach (var fn in footnoteElements)
                {
                    // Parse the target page number (Aspose.Pdf uses 1‑based indexing).
                    int pageNum = (int?)fn.Attribute("page") ?? 0;
                    if (pageNum < 1 || pageNum > pdfDoc.Pages.Count)
                        continue; // Skip invalid page references.

                    // Optional footnote number (e.g., "1", "2").
                    string number = (string?)fn.Attribute("number") ?? string.Empty;
                    // The actual footnote text.
                    string text = fn.Value?.Trim() ?? string.Empty;

                    // Build the displayed footnote string.
                    string footnoteContent = string.IsNullOrEmpty(number)
                        ? text
                        : $"{number}. {text}";

                    // Create a TextFragment for the footnote.
                    TextFragment tf = new TextFragment(footnoteContent);
                    tf.TextState.FontSize = 8; // Smaller font for footnotes.
                    tf.TextState.Font = FontRepository.FindFont("Arial"); // Use a common font.

                    // Position the footnote near the bottom left of the page.
                    // Adjust Y coordinate as needed for your layout.
                    tf.Position = new Position(40, 20);

                    // Retrieve the target page (1‑based index) and add the fragment.
                    Page page = pdfDoc.Pages[pageNum];
                    page.Paragraphs.Add(tf);
                }
            }

            // Save the modified PDF. No SaveOptions needed for PDF output.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Footnotes inserted and saved to '{outputPath}'.");
    }
}