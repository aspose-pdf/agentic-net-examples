using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Step 1: create a sample PDF that contains a simple table
        using (Document sampleDoc = new Document())
        {
            Page page = sampleDoc.Pages.Add();

            Table table = new Table();
            table.ColumnWidths = "200 200";

            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell 1");
            dataRow.Cells.Add("Cell 2");

            page.Paragraphs.Add(table);

            // Guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                sampleDoc.Save(inputPath);
                Console.WriteLine($"Sample PDF saved to '{inputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping PDF creation on non‑Windows platform (requires GDI+)." );
                return; // No further processing possible without the input file
            }
        }

        // Step 2: load the PDF and remove all tables while keeping other content
        using (Document doc = new Document(inputPath))
        {
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc);

            // Remove up to four tables (evaluation mode limitation)
            int tablesToRemove = Math.Min(absorber.TableList.Count, 4);
            for (int i = 0; i < tablesToRemove; i++)
            {
                AbsorbedTable absorbedTable = absorber.TableList[i];
                absorber.Remove(absorbedTable);
            }

            // Guard against missing GDI+ on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Output PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping PDF save on non‑Windows platform (requires GDI+)." );
            }
        }
    }
}
