using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;   // for BorderInfo, MarginInfo, BorderSide

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPageNumber = 2; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Validate the requested page number
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine("Invalid page number.");
                return;
            }

            // Access the specific page
            Page page = doc.Pages[targetPageNumber];

            // Create a table with three columns
            Table table = new Table
            {
                ColumnWidths = "100 150 200", // widths for three columns
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black), // float literal
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ----- Header row -----
            Row headerRow = table.Rows.Add(); // adds a new row
            Cell h1 = headerRow.Cells.Add();
            h1.Paragraphs.Add(new TextFragment("Header 1"));
            Cell h2 = headerRow.Cells.Add();
            h2.Paragraphs.Add(new TextFragment("Header 2"));
            Cell h3 = headerRow.Cells.Add();
            h3.Paragraphs.Add(new TextFragment("Header 3"));

            // ----- Data row -----
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add().Paragraphs.Add(new TextFragment("Row1 Col1"));
            dataRow.Cells.Add().Paragraphs.Add(new TextFragment("Row1 Col2"));
            dataRow.Cells.Add().Paragraphs.Add(new TextFragment("Row1 Col3"));

            // Insert the table into the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the modified document (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table added to page {targetPageNumber} and saved as '{outputPath}'.");
    }
}
