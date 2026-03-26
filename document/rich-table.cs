using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        const string outputPath = "rich_table.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Table table = new Table();
            table.ColumnWidths = "150 150";
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);
            table.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);
            table.Left = 50;
            table.Top = 700;

            // Header row with bold red and italic blue text
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add();
            TextFragment tfHeader1 = new TextFragment("Bold Red");
            tfHeader1.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            tfHeader1.TextState.FontSize = 12;
            tfHeader1.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            headerCell1.Paragraphs.Add(tfHeader1);

            Cell headerCell2 = headerRow.Cells.Add();
            TextFragment tfHeader2 = new TextFragment("Italic Blue");
            tfHeader2.TextState.Font = FontRepository.FindFont("Helvetica-Oblique");
            tfHeader2.TextState.FontSize = 12;
            tfHeader2.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            headerCell2.Paragraphs.Add(tfHeader2);

            // Data row with normal black and underlined green text
            Row dataRow = table.Rows.Add();
            Cell dataCell1 = dataRow.Cells.Add();
            TextFragment tfData1 = new TextFragment("Normal Black");
            tfData1.TextState.Font = FontRepository.FindFont("Helvetica");
            tfData1.TextState.FontSize = 12;
            tfData1.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            dataCell1.Paragraphs.Add(tfData1);

            Cell dataCell2 = dataRow.Cells.Add();
            TextFragment tfData2 = new TextFragment("Underline Green");
            tfData2.TextState.Font = FontRepository.FindFont("Helvetica");
            tfData2.TextState.FontSize = 12;
            tfData2.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
            tfData2.TextState.Underline = true;
            dataCell2.Paragraphs.Add(tfData2);

            page.Paragraphs.Add(table);

            // Save the document – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}'.");
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
