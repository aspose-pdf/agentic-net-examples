using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputPdf = "form_report.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF (no facade needed for form enumeration)
        Document srcDoc = new Document(inputPdf);

        // Create a new PDF document for the report
        using (Document reportDoc = new Document())
        {
            Page page = reportDoc.Pages.Add();

            Table table = new Table
            {
                ColumnWidths = "150 150 200",
                Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add("Field Name");
            header.Cells.Add("Field Type");
            header.Cells.Add("Value");
            foreach (Cell cell in header.Cells)
            {
                cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                cell.DefaultCellTextState = new TextState
                {
                    FontSize = 12,
                    FontStyle = FontStyles.Bold
                };
            }

            // Data rows – iterate over each form field
            foreach (Field field in srcDoc.Form.Fields)
            {
                Row row = table.Rows.Add();

                // Field name
                row.Cells.Add(field.PartialName);

                // Field type (simple class name)
                string fieldType = field.GetType().Name;
                row.Cells.Add(fieldType);

                // Field value (null‑safe)
                string valueStr = field.Value?.ToString() ?? string.Empty;
                row.Cells.Add(valueStr);
            }

            page.Paragraphs.Add(table);
            reportDoc.Save(outputPdf);
        }

        Console.WriteLine($"Form fields report saved to '{outputPdf}'.");
    }
}
