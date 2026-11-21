using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text; // needed for TextFragment

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new table with three columns
            Table newTable = new Table();
            newTable.ColumnWidths = "150 150 150";

            // Set default cell border for all cells (border on all sides, 0.5 pt, black)
            newTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black);

            // Optional: set default cell padding if desired
            // newTable.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);

            // Add a header row
            Row header = new Row();
            Cell headerCell1 = new Cell();
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            header.Cells.Add(headerCell1);

            Cell headerCell2 = new Cell();
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            header.Cells.Add(headerCell2);

            Cell headerCell3 = new Cell();
            headerCell3.Paragraphs.Add(new TextFragment("Header 3"));
            header.Cells.Add(headerCell3);

            newTable.Rows.Add(header);

            // Add a data row
            Row data = new Row();
            Cell dataCell1 = new Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Row 1, Col 1"));
            data.Cells.Add(dataCell1);

            Cell dataCell2 = new Cell();
            dataCell2.Paragraphs.Add(new TextFragment("Row 1, Col 2"));
            data.Cells.Add(dataCell2);

            Cell dataCell3 = new Cell();
            dataCell3.Paragraphs.Add(new TextFragment("Row 1, Col 3"));
            data.Cells.Add(dataCell3);

            newTable.Rows.Add(data);

            // Access page two (1‑based indexing). Ensure the document has at least two pages.
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }
            Page pageTwo = doc.Pages[2];

            // Find the first existing table on page two and remove it
            Table existingTable = null;
            foreach (var paragraph in pageTwo.Paragraphs)
            {
                if (paragraph is Table tbl)
                {
                    existingTable = tbl;
                    break;
                }
            }
            if (existingTable != null)
            {
                pageTwo.Paragraphs.Remove(existingTable);
            }

            // Insert the new table at the end of the page's paragraph collection
            pageTwo.Paragraphs.Add(newTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
