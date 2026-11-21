using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (or add one if the document is empty)
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // Create a table using the core Table API
            Table table = new Table
            {
                // Example column widths – adjust as needed
                ColumnWidths = "200",
                // Optional: default cell padding for the whole table
                DefaultCellPadding = new MarginInfo(2, 2, 2, 2),
                // Optional: border for visual reference
                Border = new BorderInfo(BorderSide.All, 0.5f, Color.Black)
            };

            // Header row
            Row headerRow = table.Rows.Add();
            Cell headerCell = headerRow.Cells.Add("Header");
            // You can style the header cell if desired
            headerCell.BackgroundColor = Color.LightGray;

            // Data row
            Row dataRow = table.Rows.Add();
            Cell dataCell = dataRow.Cells.Add("Data cell with margins");

            // Configure cell margins (left, bottom, right, top) using MarginInfo
            dataCell.Margin = new MarginInfo(5, 3, 5, 3);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
