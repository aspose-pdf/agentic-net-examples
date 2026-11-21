using System;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePdf = "template.pdf";   // optional template to merge
        const string tempPdf     = "subset.pdf";    // intermediate PDF
        const string finalPdf    = "final_output.pdf";

        // ------------------------------------------------------------
        // 1️⃣  Create a DataTable with sample data (replace with your own
        //     data source if you add Aspose.Cells later).
        // ------------------------------------------------------------
        DataTable dataTable = GetSampleData();

        // ------------------------------------------------------------
        // 2️⃣  Filter rows – keep only rows where column "Status" = "Active"
        // ------------------------------------------------------------
        DataView view = new DataView(dataTable);
        view.RowFilter = "Status = 'Active'";
        DataTable filteredTable = view.ToTable();

        // ------------------------------------------------------------
        // 3️⃣  Build a PDF document that contains a table with the filtered data
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            Page page = pdfDoc.Pages.Add();

            Table pdfTable = new Table
            {
                // Give each column a fixed width of 100 points (customise as needed)
                ColumnWidths = string.Join(" ", Enumerable.Repeat("100", filteredTable.Columns.Count))
            };

            // ----- Header row ---------------------------------------------------
            Row headerRow = pdfTable.Rows.Add();
            foreach (DataColumn col in filteredTable.Columns)
            {
                Cell headerCell = headerRow.Cells.Add();
                headerCell.Paragraphs.Add(new TextFragment(col.ColumnName)
                {
                    TextState = { FontSize = 12, FontStyle = FontStyles.Bold }
                });
            }

            // ----- Data rows ----------------------------------------------------
            foreach (DataRow dr in filteredTable.Rows)
            {
                Row dataRow = pdfTable.Rows.Add();
                foreach (object item in dr.ItemArray)
                {
                    Cell dataCell = dataRow.Cells.Add();
                    dataCell.Paragraphs.Add(new TextFragment(item?.ToString() ?? string.Empty)
                    {
                        TextState = { FontSize = 10 }
                    });
                }
            }

            page.Paragraphs.Add(pdfTable);
            pdfDoc.Save(tempPdf);
        }

        // ------------------------------------------------------------
        // 4️⃣  (Optional) Merge the generated PDF with a template PDF
        // ------------------------------------------------------------
        if (File.Exists(templatePdf))
        {
            // PdfFileEditor does NOT implement IDisposable – do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();
            editor.Concatenate(new[] { templatePdf, tempPdf }, finalPdf);
            Console.WriteLine($"Merged PDF saved to '{finalPdf}'.");
        }
        else
        {
            // No template – just rename the intermediate file
            File.Copy(tempPdf, finalPdf, overwrite: true);
            Console.WriteLine($"PDF saved to '{finalPdf}'.");
        }

        // Clean‑up the temporary file
        if (File.Exists(tempPdf))
            File.Delete(tempPdf);
    }

    // --------------------------------------------------------------------
    // Helper: creates a simple DataTable with sample rows. Replace this
    // method with real Excel‑to‑DataTable logic (e.g., using Aspose.Cells) if
    // you add the missing NuGet package later.
    // --------------------------------------------------------------------
    private static DataTable GetSampleData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Status", typeof(string));

        dt.Rows.Add(1, "Alice", "Active");
        dt.Rows.Add(2, "Bob", "Inactive");
        dt.Rows.Add(3, "Charlie", "Active");
        return dt;
    }
}
