using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form_input.pdf";   // PDF containing the AcroForm
        const string outputPdfPath = "form_report.pdf"; // Generated report

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF that contains the form fields
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            // Initialize FormEditor – required by the task (even if we don't use its GetFormFields method)
            using (FormEditor formEditor = new FormEditor(sourceDoc))
            {
                // Retrieve all field names from the form using the Document.Form collection
                string[] fieldNames = sourceDoc.Form.Fields
                                                .Select(f => f.PartialName)
                                                .ToArray();

                // Create a new PDF document that will hold the report
                using (Document reportDoc = new Document())
                {
                    // Add a page to the report document
                    Page reportPage = reportDoc.Pages.Add();

                    // Create a table with three columns: Name, Type, Value
                    Table table = new Table
                    {
                        // Optional: set column widths (percentage of page width)
                        ColumnWidths = "30% 20% 50%"
                    };

                    // Add a header row
                    Row header = table.Rows.Add();
                    AddCell(header, "Field Name", true);
                    AddCell(header, "Field Type", true);
                    AddCell(header, "Value", true);

                    // Iterate over each form field name and add a row with its details
                    foreach (string fieldName in fieldNames)
                    {
                        // Find the actual field object in the document
                        var field = sourceDoc.Form.Fields.FirstOrDefault(f => f.PartialName == fieldName);
                        if (field == null)
                            continue; // safety check

                        // Determine the field type (class name) and its current value
                        string fieldType = field.GetType().Name;
                        string fieldValue = field.Value?.ToString() ?? string.Empty;

                        Row row = table.Rows.Add();
                        AddCell(row, fieldName, false);
                        AddCell(row, fieldType, false);
                        AddCell(row, fieldValue, false);
                    }

                    // Add the table to the page
                    reportPage.Paragraphs.Add(table);

                    // Save the report PDF
                    reportDoc.Save(outputPdfPath);
                }
            }
        }

        Console.WriteLine($"Form fields report saved to '{outputPdfPath}'.");
    }

    // Helper method to add a cell to a table row with optional header styling
    private static void AddCell(Row row, string text, bool isHeader)
    {
        Cell cell = row.Cells.Add();
        TextFragment fragment = new TextFragment(text);
        if (isHeader)
        {
            // Header styling: bold font, gray background
            fragment.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Black;
            cell.BackgroundColor = Color.LightGray;
        }
        else
        {
            // Regular cell styling
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 10;
            fragment.TextState.ForegroundColor = Color.Black;
        }
        cell.Paragraphs.Add(fragment);
        cell.Margin = new MarginInfo(5, 5, 5, 5);
    }
}
