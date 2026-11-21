using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // HtmlFragment, TextFragment, etc.
using Aspose.Pdf.Drawing;       // Image, ImagePlacement

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string svgImagePath  = "image.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(svgImagePath))
        {
            Console.Error.WriteLine($"SVG image not found: {svgImagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure there is at least one page to add the table to
            Page page = doc.Pages[1];

            // Create a table with two columns
            Table table = new Table
            {
                // Define column widths (adjust as needed)
                ColumnWidths = "100 200"
            };

            // Add a single row
            Row row = table.Rows.Add();

            // ----- First cell: embed the SVG image -----
            Cell imageCell = row.Cells.Add();

            // Load the SVG file into an Image object
            using (FileStream svgStream = File.OpenRead(svgImagePath))
            {
                Image svgImage = new Image
                {
                    ImageStream = svgStream,
                    // Optional: set width/height or scaling
                    FixWidth = 80,
                    // Preserve the SVG format; Aspose.Pdf detects it automatically
                };

                // Add the image to the cell's paragraph collection
                imageCell.Paragraphs.Add(svgImage);
            }

            // ----- Second cell: add HTML-formatted text -----
            Cell htmlCell = row.Cells.Add();

            // Create an HtmlFragment containing the desired HTML markup
            HtmlFragment html = new HtmlFragment("<b>Accessible HTML Text</b> <i>with styling</i>");

            // Add the HTML fragment to the cell
            htmlCell.Paragraphs.Add(html);

            // Add the completed table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with table to '{outputPdfPath}'.");
    }
}