using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "styled_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure its layout
            Table table = new Table();

            // Define three equal column widths (example)
            table.ColumnWidths = "150 150 150";

            // Set a uniform border for the whole table
            table.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.DarkGray);

            // Set default cell border, padding and text style
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.LightGray);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            table.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black
            };

            // ----- Header Row -----
            Row headerRow = new Row();
            // Header cell 1
            Cell headerCell1 = new Cell();
            headerCell1.BackgroundColor = Aspose.Pdf.Color.LightBlue;
            headerCell1.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Blue);
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            // Header cell 2
            Cell headerCell2 = new Cell();
            headerCell2.BackgroundColor = Aspose.Pdf.Color.LightBlue;
            headerCell2.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Blue);
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            // Header cell 3
            Cell headerCell3 = new Cell();
            headerCell3.BackgroundColor = Aspose.Pdf.Color.LightBlue;
            headerCell3.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Blue);
            headerCell3.Paragraphs.Add(new TextFragment("Header 3"));

            // Add cells to header row
            headerRow.Cells.Add(headerCell1);
            headerRow.Cells.Add(headerCell2);
            headerRow.Cells.Add(headerCell3);

            // Add header row to table
            table.Rows.Add(headerRow);

            // ----- Data Row 1 -----
            Row dataRow1 = new Row();

            Cell dataCell11 = new Cell();
            dataCell11.BackgroundColor = Aspose.Pdf.Color.White;
            dataCell11.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            TextFragment tf11 = new TextFragment("Row1-Col1");
            tf11.TextState.Font = FontRepository.FindFont("Helvetica");
            tf11.TextState.FontSize = 11;
            tf11.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGreen;
            dataCell11.Paragraphs.Add(tf11);

            Cell dataCell12 = new Cell();
            dataCell12.BackgroundColor = Aspose.Pdf.Color.White;
            dataCell12.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            dataCell12.Paragraphs.Add(new TextFragment("Row1-Col2"));

            Cell dataCell13 = new Cell();
            dataCell13.BackgroundColor = Aspose.Pdf.Color.White;
            dataCell13.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            dataCell13.Paragraphs.Add(new TextFragment("Row1-Col3"));

            dataRow1.Cells.Add(dataCell11);
            dataRow1.Cells.Add(dataCell12);
            dataRow1.Cells.Add(dataCell13);
            table.Rows.Add(dataRow1);

            // ----- Data Row 2 -----
            Row dataRow2 = new Row();

            Cell dataCell21 = new Cell();
            dataCell21.BackgroundColor = Aspose.Pdf.Color.LightYellow;
            dataCell21.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            dataCell21.Paragraphs.Add(new TextFragment("Row2-Col1"));

            Cell dataCell22 = new Cell();
            dataCell22.BackgroundColor = Aspose.Pdf.Color.LightYellow;
            dataCell22.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            dataCell22.Paragraphs.Add(new TextFragment("Row2-Col2"));

            Cell dataCell23 = new Cell();
            dataCell23.BackgroundColor = Aspose.Pdf.Color.LightYellow;
            dataCell23.Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray);
            dataCell23.Paragraphs.Add(new TextFragment("Row2-Col3"));

            dataRow2.Cells.Add(dataCell21);
            dataRow2.Cells.Add(dataCell22);
            dataRow2.Cells.Add(dataCell23);
            table.Rows.Add(dataRow2);

            // Add the configured table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled table saved to '{outputPath}'.");
    }
}