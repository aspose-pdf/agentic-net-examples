using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (with form fields) and the generated report PDF
        const string sourcePdfPath = "form_input.pdf";
        const string reportPdfPath = "form_report.pdf";

        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the source PDF and retrieve its form fields using Document.Form
        // -----------------------------------------------------------------
        Document srcDoc = new Document(sourcePdfPath);
        // Document.Form implements IEnumerable<Field>
        var formFields = srcDoc.Form; // collection of Field objects

        // -----------------------------------------------------------------
        // 2. Create a new PDF document that will contain the report table
        // -----------------------------------------------------------------
        using (Document reportDoc = new Document())
        {
            // Add a single page to the report document
            Page page = reportDoc.Pages.Add();

            // Create a table with three columns: Name, Type, Value
            Table table = new Table
            {
                // Define column widths (in points). Adjust as needed.
                ColumnWidths = "150 100 250",
                // Optional: set border for visual clarity
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add the table to the page
            page.Paragraphs.Add(table);

            // -----------------------------------------------------------------
            // 3. Add header row
            // -----------------------------------------------------------------
            Row headerRow = new Row();
            headerRow.BackgroundColor = Color.LightGray; // Aspose.Pdf.Color

            Cell CreateHeaderCell(string text)
            {
                var cell = new Cell();
                var tf = new TextFragment(text)
                {
                    TextState = { FontSize = 12, FontStyle = FontStyles.Bold }
                };
                cell.Paragraphs.Add(tf);
                return cell;
            }

            headerRow.Cells.Add(CreateHeaderCell("Field Name"));
            headerRow.Cells.Add(CreateHeaderCell("Field Type"));
            headerRow.Cells.Add(CreateHeaderCell("Field Value"));
            table.Rows.Add(headerRow);

            // -----------------------------------------------------------------
            // 4. Populate the table with form field information
            // -----------------------------------------------------------------
            foreach (Field field in formFields)
            {
                Row row = new Row();

                // Field Name
                var nameCell = new Cell();
                nameCell.Paragraphs.Add(new TextFragment(field.FullName ?? string.Empty));
                row.Cells.Add(nameCell);

                // Field Type (exact class name)
                var typeCell = new Cell();
                typeCell.Paragraphs.Add(new TextFragment(field.GetType().Name));
                row.Cells.Add(typeCell);

                // Field Value – handle known field types, fallback to empty string
                string valueText = string.Empty;
                switch (field)
                {
                    case TextBoxField tb:
                        valueText = tb.Value ?? string.Empty;
                        break;
                    case CheckboxField cb:
                        valueText = cb.Checked.ToString();
                        break;
                    case RadioButtonField rb:
                        // Selected is 1‑based index of the chosen option; 0 means none
                        valueText = rb.Selected > 0 ? rb.Selected.ToString() : string.Empty;
                        break;
                    case ListBoxField lb:
                        if (lb.SelectedItems != null && lb.SelectedItems.Length > 0)
                            valueText = string.Join(", ", lb.SelectedItems);
                        break;
                    case ComboBoxField cbx:
                        valueText = cbx.Value ?? string.Empty;
                        break;
                    case SignatureField sig:
                        valueText = "<Signature>";
                        break;
                    default:
                        // For any other field types attempt a generic ToString()
                        valueText = field.ToString();
                        break;
                }

                var valueCell = new Cell();
                valueCell.Paragraphs.Add(new TextFragment(valueText));
                row.Cells.Add(valueCell);

                table.Rows.Add(row);
            }

            // -----------------------------------------------------------------
            // 5. Save the report PDF
            // -----------------------------------------------------------------
            reportDoc.Save(reportPdfPath);
        }

        Console.WriteLine($"Form field report generated: {reportPdfPath}");
    }
}
