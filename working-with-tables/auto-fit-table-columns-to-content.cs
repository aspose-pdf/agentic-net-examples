using System;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_autofit.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a table and enable column auto‑fit behavior
            Table table = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent
            };

            // Optionally set a border around the whole table
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Header row
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = new Cell();
            headerCell1.Paragraphs.Add(new TextFragment("Product"));
            headerCell1.DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica-Bold") };
            headerRow.Cells.Add(headerCell1);

            Cell headerCell2 = new Cell();
            headerCell2.Paragraphs.Add(new TextFragment("Description"));
            headerCell2.DefaultCellTextState = new TextState { FontSize = 12, Font = FontRepository.FindFont("Helvetica-Bold") };
            headerRow.Cells.Add(headerCell2);

            // Data row with long content to demonstrate auto‑fit
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = new Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Widget A"));
            dataRow.Cells.Add(dataCell1);

            Cell dataCell2 = new Cell();
            dataCell2.Paragraphs.Add(new TextFragment(
                "This is a very long description that should cause the column to adjust its width automatically to fit the content without truncation."));
            dataRow.Cells.Add(dataCell2);

            // Add the table to the page and save the document
            page.Paragraphs.Add(table);
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AutoFitToContent table saved to '{outputPath}'.");
    }
}
