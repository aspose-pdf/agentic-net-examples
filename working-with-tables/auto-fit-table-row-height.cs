using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_auto_row.pdf";

        // Create a simple PDF if the input does not exist
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Add a page if the document is empty
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a table and add it to the first page
            Table table = new Table
            {
                // Define two columns with relative widths
                ColumnWidths = "200 300",
                // Optional: set border for visual clarity
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Build the header row
            Row headerRow = table.Rows.Add();
            headerRow.BackgroundColor = Color.LightGray;
            headerRow.Cells.Add("Description");
            headerRow.Cells.Add("Details");

            // Build the body row. Row height auto‑fits by default; no explicit Height property exists.
            Row bodyRow = table.Rows.Add();
            // If you need a minimum height you can set it, e.g., bodyRow.MinRowHeight = 20;
            bodyRow.Cells.Add("Short text.");
            bodyRow.Cells.Add("This is a much longer piece of text that should cause the row to expand automatically to fit all the content without clipping.");

            // Add the table to the page's paragraphs collection
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
