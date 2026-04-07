using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class AutoFitTableExample
{
    public static void Main()
    {
        // Create a simple PDF to work with
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the PDF and add a table with AutoFitToContent
        using (Document doc = new Document("input.pdf"))
        {
            // Create a table
            Table table = new Table();

            // Apply AutoFit behavior so the columns resize to the cell contents
            // In Aspose.Pdf the correct property is ColumnAdjustment, not AutoFitBehavior
            table.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;

            // Add first row (header)
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");

            // Add second row (data)
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("This is a longer piece of text that will cause the column to expand");
            dataRow.Cells.Add("Short");

            // Add the table to the first page
            doc.Pages[1].Paragraphs.Add(table);

            // Save the result – guard the call on non‑Windows platforms where libgdiplus may be missing
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save("output.pdf");
                Console.WriteLine("PDF saved to 'output.pdf'.");
            }
            else
            {
                Console.WriteLine("Skipping Document.Save – GDI+ (libgdiplus) is not available on this platform.");
            }
        }
    }
}
