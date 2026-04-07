using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        // Create a new PDF document
        using (Document document = new Document())
        {
            // Add a page
            Page page = document.Pages.Add();

            // Create a table with three columns
            Table table = new Table();
            table.ColumnWidths = "150 150 150";

            // Create header row
            Row headerRow = new Row();
            // Set background color (light gray) – values must be in the 0..1 range
            headerRow.BackgroundColor = Aspose.Pdf.Color.FromRgb(220.0 / 255, 220.0 / 255, 220.0 / 255);
            // Set default text state for header cells (bold font)
            TextState headerTextState = new TextState();
            headerTextState.Font = FontRepository.FindFont("Arial");
            headerTextState.FontSize = 12;
            headerTextState.FontStyle = FontStyles.Bold;
            headerRow.DefaultCellTextState = headerTextState;

            // Add cells to header row
            Cell headerCell1 = new Cell();
            headerCell1.Paragraphs.Add(new TextFragment("Product"));
            headerRow.Cells.Add(headerCell1);

            Cell headerCell2 = new Cell();
            headerCell2.Paragraphs.Add(new TextFragment("Quantity"));
            headerRow.Cells.Add(headerCell2);

            Cell headerCell3 = new Cell();
            headerCell3.Paragraphs.Add(new TextFragment("Price"));
            headerRow.Cells.Add(headerCell3);

            // Add header row to table
            table.Rows.Add(headerRow);

            // Add a sample data row
            Row dataRow = new Row();
            Cell dataCell1 = new Cell();
            dataCell1.Paragraphs.Add(new TextFragment("Apple"));
            dataRow.Cells.Add(dataCell1);

            Cell dataCell2 = new Cell();
            dataCell2.Paragraphs.Add(new TextFragment("10"));
            dataRow.Cells.Add(dataCell2);

            Cell dataCell3 = new Cell();
            dataCell3.Paragraphs.Add(new TextFragment("$1.00"));
            dataRow.Cells.Add(dataCell3);

            table.Rows.Add(dataRow);

            // Add table to page
            page.Paragraphs.Add(table);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            const string outputPath = "output.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                document.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    document.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'. (non‑Windows platform, libgdiplus may be required)");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF was not saved.");
                }
            }
        }
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
