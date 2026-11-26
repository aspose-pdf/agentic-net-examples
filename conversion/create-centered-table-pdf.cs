using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_output.pdf";

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table and set alignment attribute
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                Alignment = Aspose.Pdf.HorizontalAlignment.Center, // Center the table on the page
                ColumnWidths = "100 100 100"                 // Define three equal columns
            };

            // ---------- Header Row ----------
            Aspose.Pdf.Row headerRow = new Aspose.Pdf.Row();

            // Header Cell 1
            Aspose.Pdf.Cell headerCell1 = new Aspose.Pdf.Cell
            {
                DefaultCellTextState = new TextState
                {
                    Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };
            headerCell1.Paragraphs.Add(new TextFragment("Header 1"));
            headerRow.Cells.Add(headerCell1);

            // Header Cell 2
            Aspose.Pdf.Cell headerCell2 = new Aspose.Pdf.Cell
            {
                DefaultCellTextState = new TextState
                {
                    Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };
            headerCell2.Paragraphs.Add(new TextFragment("Header 2"));
            headerRow.Cells.Add(headerCell2);

            // Header Cell 3
            Aspose.Pdf.Cell headerCell3 = new Aspose.Pdf.Cell
            {
                DefaultCellTextState = new TextState
                {
                    Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 12,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };
            headerCell3.Paragraphs.Add(new TextFragment("Header 3"));
            headerRow.Cells.Add(headerCell3);

            // Add header row to the table
            table.Rows.Add(headerRow);

            // ---------- Data Row ----------
            Aspose.Pdf.Row dataRow = new Aspose.Pdf.Row();

            // Data Cell 1
            Aspose.Pdf.Cell dataCell1 = new Aspose.Pdf.Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Cell 1"));
            dataRow.Cells.Add(dataCell1);

            // Data Cell 2
            Aspose.Pdf.Cell dataCell2 = new Aspose.Pdf.Cell();
            dataCell2.Paragraphs.Add(new TextFragment("Cell 2"));
            dataRow.Cells.Add(dataCell2);

            // Data Cell 3
            Aspose.Pdf.Cell dataCell3 = new Aspose.Pdf.Cell();
            dataCell3.Paragraphs.Add(new TextFragment("Cell 3"));
            dataRow.Cells.Add(dataCell3);

            // Add data row to the table
            table.Rows.Add(dataRow);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
