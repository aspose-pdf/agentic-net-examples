using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        string svgPath = "sample.svg";
        // Verify that the SVG file exists before proceeding.
        if (!File.Exists(svgPath))
        {
            Console.WriteLine($"SVG file not found: {svgPath}");
            return;
        }

        using (Document doc = new Document())
        {
            // Add a page to the document.
            Page page = doc.Pages.Add();

            // Create a table with two columns.
            Table table = new Table();
            table.ColumnWidths = "200 200"; // widths in points.

            // Add a row to the table.
            Row row = table.Rows.Add();

            // First cell – simple text.
            Cell textCell = row.Cells.Add();
            TextFragment textFragment = new TextFragment("SVG Image:");
            textCell.Paragraphs.Add(textFragment);

            // Second cell – SVG image.
            Cell imageCell = row.Cells.Add();
            Image svgImage = new Image();
            svgImage.File = svgPath;
            svgImage.FileType = ImageFileType.Svg;
            // Optional: set display size.
            svgImage.FixWidth = 150;
            svgImage.FixHeight = 150;
            imageCell.Paragraphs.Add(svgImage);

            // Add the table to the page.
            page.Paragraphs.Add(table);

            // Save the PDF document.
            doc.Save("output.pdf");
        }
    }
}