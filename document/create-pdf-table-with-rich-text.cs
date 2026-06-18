using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

#pragma warning disable NU1903 // suppress known high‑severity vulnerability warning for Microsoft.Bcl.Memory

class Program
{
    static void Main()
    {
        const string outputPath = "rich_table.pdf";

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure its appearance
            Table table = new Table
            {
                // Define three columns with specific widths
                ColumnWidths = "100 150 200",
                // Apply a thin black border to all cells by default
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                // Add some padding inside each cell
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ---------- Header Row ----------
            Row header = table.Rows.Add();

            // Helper to create a header cell with bold white text on a dark background
            void AddHeaderCell(string text, Cell targetCell)
            {
                TextFragment tf = new TextFragment(text);
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 12;
                tf.TextState.FontStyle = FontStyles.Bold;
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.White;

                targetCell.BackgroundColor = Aspose.Pdf.Color.DarkBlue;
                targetCell.Paragraphs.Add(tf);
            }

            Cell hCell1 = header.Cells.Add();
            AddHeaderCell("Product", hCell1);

            Cell hCell2 = header.Cells.Add();
            AddHeaderCell("Price", hCell2);

            Cell hCell3 = header.Cells.Add();
            AddHeaderCell("Description", hCell3);

            // ---------- Data Row ----------
            Row dataRow = table.Rows.Add();

            // Cell 1 – plain text
            Cell dCell1 = dataRow.Cells.Add();
            TextFragment tfPlain = new TextFragment("Widget A");
            tfPlain.TextState.Font = FontRepository.FindFont("Helvetica");
            tfPlain.TextState.FontSize = 10;
            tfPlain.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            dCell1.Paragraphs.Add(tfPlain);

            // Cell 2 – colored italic text
            Cell dCell2 = dataRow.Cells.Add();
            TextFragment tfRich = new TextFragment("€99.99");
            tfRich.TextState.Font = FontRepository.FindFont("Helvetica");
            tfRich.TextState.FontSize = 10;
            tfRich.TextState.FontStyle = FontStyles.Italic;
            tfRich.TextState.ForegroundColor = Aspose.Pdf.Color.Green;
            dCell2.Paragraphs.Add(tfRich);

            // Cell 3 – multiple fragments (bold + normal)
            Cell dCell3 = dataRow.Cells.Add();

            TextFragment tfBoldPart = new TextFragment("High-quality ");
            tfBoldPart.TextState.Font = FontRepository.FindFont("Helvetica");
            tfBoldPart.TextState.FontSize = 10;
            tfBoldPart.TextState.FontStyle = FontStyles.Bold;
            tfBoldPart.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            TextFragment tfNormalPart = new TextFragment("widget for testing.");
            tfNormalPart.TextState.Font = FontRepository.FindFont("Helvetica");
            tfNormalPart.TextState.FontSize = 10;
            tfNormalPart.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            dCell3.Paragraphs.Add(tfBoldPart);
            dCell3.Paragraphs.Add(tfNormalPart);

            // Add the fully constructed table to the page
            page.Paragraphs.Add(table);

            // Save the document – guard against missing libgdiplus on non‑Windows platforms
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
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                }
            }
        }

        Console.WriteLine($"PDF with rich‑text table saved to '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
#pragma warning restore NU1903