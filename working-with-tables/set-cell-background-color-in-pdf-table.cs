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

        // Load existing PDF if present; otherwise create a new document with a blank page.
        Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document();
        if (doc.Pages.Count == 0)
            doc.Pages.Add();

        // Work with the first page.
        Page page = doc.Pages[1];

        // Create a table with two equal‑width columns.
        Table table = new Table
        {
            ColumnWidths = "200 200",
            Border = new BorderInfo(BorderSide.All, 0.5f)
        };

        // ----- Header row -----
        Row headerRow = table.Rows.Add();
        Cell headerCell1 = headerRow.Cells.Add("Item");
        Cell headerCell2 = headerRow.Cells.Add("Value");
        headerCell1.BackgroundColor = Color.LightGray;
        headerCell2.BackgroundColor = Color.LightGray;

        // ----- Data row -----
        Row dataRow = table.Rows.Add();
        Cell dataCell1 = dataRow.Cells.Add("Widget A");
        Cell dataCell2 = dataRow.Cells.Add("$50,000");
        // Set background colour of the second cell as required by the task.
        dataCell2.BackgroundColor = Color.LightGray;

        // Add the table to the page.
        page.Paragraphs.Add(table);

        // Save the PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Saved PDF with colored cell to '{outputPath}'.");
    }
}
