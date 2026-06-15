using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TextFragment, Position

class Program
{
    static void Main()
    {
        // Input XML file that contains XSLT/XSL-FO logic with pagination placeholders (e.g., "#")
        const string inputXmlPath = "input.xml";
        // Output PDF file with automatic page numbers applied
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputXmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputXmlPath}");
            return;
        }

        // Load the XML using XmlLoadOptions (required for XML/XSLT processing)
        // XmlLoadOptions resides in the Aspose.Pdf namespace, so no extra using is needed.
        using (Document doc = new Document(inputXmlPath, new XmlLoadOptions()))
        {
            // ------------------------------------------------------------
            // Add automatic page numbers.
            // Aspose.Pdf does not expose UpdatePagination without the Facades
            // namespace (which is prohibited by the task). Therefore we add the
            // numbers manually – this works for any PDF generated from the XML.
            // ------------------------------------------------------------
            const float marginFromBottom = 20f; // points from the bottom edge
            const float marginFromRight = 40f;  // points from the right edge
            const int fontSize = 10;

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Create a text fragment with the current page number.
                TextFragment tf = new TextFragment($"Page {i} of {doc.Pages.Count}")
                {
                    TextState = { FontSize = fontSize, Font = FontRepository.FindFont("Helvetica") },
                    // Position the fragment near the bottom‑right corner.
                    // X = page width - margin, Y = margin from bottom.
                    Position = new Position(page.PageInfo.Width - marginFromRight, marginFromBottom)
                };
                page.Paragraphs.Add(tf);
            }

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with automatic page numbers saved to '{outputPdfPath}'.");
    }
}
