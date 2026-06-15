using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "MergedHeaderTable.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Table table = new Table { ColumnWidths = "150 150 150" };

            // -------------------------------------------------
            // First header row – a single cell that spans all 3 columns
            // -------------------------------------------------
            Row mergedHeaderRow = table.Rows.Add();
            Cell mergedHeaderCell = mergedHeaderRow.Cells.Add();
            mergedHeaderCell.ColSpan = 3;
            mergedHeaderCell.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 16,
                ForegroundColor = Aspose.Pdf.Color.Blue,
                FontStyle = FontStyles.Bold
            };
            mergedHeaderCell.Paragraphs.Add(new TextFragment("Merged Header Across All Columns"));

            // -------------------------------------------------
            // Second header row – separate column titles
            // -------------------------------------------------
            Row columnHeaderRow = table.Rows.Add();

            Cell col1Header = columnHeaderRow.Cells.Add();
            col1Header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black,
                FontStyle = FontStyles.Bold
            };
            col1Header.Paragraphs.Add(new TextFragment("Column 1"));

            Cell col2Header = columnHeaderRow.Cells.Add();
            col2Header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black,
                FontStyle = FontStyles.Bold
            };
            col2Header.Paragraphs.Add(new TextFragment("Column 2"));

            Cell col3Header = columnHeaderRow.Cells.Add();
            col3Header.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.Black,
                FontStyle = FontStyles.Bold
            };
            col3Header.Paragraphs.Add(new TextFragment("Column 3"));

            // -------------------------------------------------
            // Add a few data rows
            // -------------------------------------------------
            for (int i = 1; i <= 5; i++)
            {
                Row dataRow = table.Rows.Add();

                Cell cell1 = dataRow.Cells.Add();
                cell1.Paragraphs.Add(new TextFragment($"Row {i} - A"));

                Cell cell2 = dataRow.Cells.Add();
                cell2.Paragraphs.Add(new TextFragment($"Row {i} - B"));

                Cell cell3 = dataRow.Cells.Add();
                cell3.Paragraphs.Add(new TextFragment($"Row {i} - C"));
            }

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // -------------------------------------------------
            // Save the PDF – guard against missing libgdiplus on non‑Windows platforms
            // -------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }

        Console.WriteLine($"PDF with merged header table saved to '{outputPath}'.");
    }

    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
