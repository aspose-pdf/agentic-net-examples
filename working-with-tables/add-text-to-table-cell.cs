using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static void Main()
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Table table = new Table();
            table.ColumnWidths = "200";

            Row row = table.Rows.Add();

            Cell cell = row.Cells.Add();

            TextFragment tf = new TextFragment("Hello, Aspose!");
            tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            tf.TextState.FontSize = 14f;

            cell.Paragraphs.Add(tf);

            page.Paragraphs.Add(table);

            string outputPath = "output.pdf";

            // Guard Document.Save on non‑Windows platforms where libgdiplus (GDI+) may be missing.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping doc.Save() because GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }
}