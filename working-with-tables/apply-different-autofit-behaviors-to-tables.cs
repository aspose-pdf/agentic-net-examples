using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to host the tables
            Page page = doc.Pages.Count > 0 ? doc.Pages[1] : doc.Pages.Add();

            // ------------------------------------------------------------
            // Table 1 – AutoFit to content (ColumnAdjustment.AutoFitToContent)
            // ------------------------------------------------------------
            Table tableFitToContent = new Table
            {
                // Position the table on the page
                Left = 50,
                Top = 500,
                // Apply the desired auto‑fit behavior
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Optional visual styling
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black),
                // Define columns (two equal columns) – ColumnWidths is a string, not a collection
                ColumnWidths = "200 200"
            };

            // Add a header row
            Row headerRow = tableFitToContent.Rows.Add();
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 1") } });
            headerRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Header 2") } });

            // Add a data row with longer content to demonstrate auto‑fit
            Row dataRow = tableFitToContent.Rows.Add();
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("This is a very long piece of text that should cause the column to expand to fit its content.") } });
            dataRow.Cells.Add(new Cell { Paragraphs = { new TextFragment("Short") } });

            // Add the first table to the page
            page.Paragraphs.Add(tableFitToContent);

            // ------------------------------------------------------------
            // Table 2 – AutoFit to window (ColumnAdjustment.AutoFitToWindow)
            // ------------------------------------------------------------
            Table tableFitToWindow = new Table
            {
                Left = 50,
                Top = 300,
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.DarkGray),
                // Define columns (three columns) – again use a space‑separated string
                ColumnWidths = "150 150 150"
            };

            // Header row
            Row headerRow2 = tableFitToWindow.Rows.Add();
            headerRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Col A") } });
            headerRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Col B") } });
            headerRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Col C") } });

            // Data rows
            Row dataRow2 = tableFitToWindow.Rows.Add();
            dataRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Item A1") } });
            dataRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Item B1") } });
            dataRow2.Cells.Add(new Cell { Paragraphs = { new TextFragment("Item C1") } });

            Row dataRow3 = tableFitToWindow.Rows.Add();
            dataRow3.Cells.Add(new Cell { Paragraphs = { new TextFragment("Item A2 with a considerably longer text that will be forced to fit within the window width.") } });
            dataRow3.Cells.Add(new Cell { Paragraphs = { new TextFragment("Item B2") } });
            dataRow3.Cells.Add(new Cell { Paragraphs = { new TextFragment("Item C2") } });

            // Add the second table to the page
            page.Paragraphs.Add(tableFitToWindow);

            // Save the modified PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tables having different AutoFit behaviors: {outputPath}");
    }
}
