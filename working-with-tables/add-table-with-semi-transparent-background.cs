using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "table_with_background.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Set table position (optional)
                Left = 50,
                Top = 500,
                // Set desired background color with opacity (e.g., semi‑transparent blue)
                // Aspose.Pdf.Color.FromArgb(alpha, red, green, blue) where alpha 0‑255
                BackgroundColor = Color.FromArgb(128, 0, 0, 255)
            };

            // Define three columns
            table.ColumnWidths = "100 150 100";

            // Add a header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8); // light gray for header
            header.Cells.Add("ID");
            header.Cells.Add("Name");
            header.Cells.Add("Quantity");

            // Add a data row
            Row data = table.Rows.Add();
            data.Cells.Add("1");
            data.Cells.Add("Apples");
            data.Cells.Add("10");

            // Add another data row
            Row data2 = table.Rows.Add();
            data2.Cells.Add("2");
            data2.Cells.Add("Oranges");
            data2.Cells.Add("20");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}