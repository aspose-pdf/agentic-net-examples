using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for Table cell text

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPdf = "output.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPdf));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table with a visible border
            Table table = new Table
            {
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Optional: define column widths so we can calculate the rendered width later
            // If you omit this, the width will be auto‑fit to content and we fall back to the page width.
            table.ColumnWidths = "200 200"; // two columns, each 200 points wide

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add(new TextFragment("Header 1"));
            header.Cells.Add(new TextFragment("Header 2"));

            // Data row
            Row data = table.Rows.Add();
            data.Cells.Add(new TextFragment("Value A"));
            data.Cells.Add(new TextFragment("Value B"));

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save a temporary file to force layout calculation.
            // The layout engine computes the actual dimensions of the table during this save.
            doc.Save("temp_layout.pdf");

            // After layout, calculate the rendered width.
            double renderedWidth = 0;
            if (!string.IsNullOrEmpty(table.ColumnWidths))
            {
                // ColumnWidths is a space‑separated list of widths (in points).
                string[] parts = table.ColumnWidths.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (float.TryParse(part, out float w))
                        renderedWidth += w;
                }
            }
            else
            {
                // If ColumnWidths is not set, fall back to the page width (minus any margins you may have applied).
                renderedWidth = page.PageInfo.Width;
            }

            Console.WriteLine($"Rendered table width: {renderedWidth} points");

            // Save the final document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}
