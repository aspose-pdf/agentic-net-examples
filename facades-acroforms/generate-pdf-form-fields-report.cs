using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form_template.pdf";   // PDF that contains the form
        const string outputPdfPath = "form_report.pdf";   // PDF report to be generated

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF that contains the form fields
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                // Bind the document to a FormEditor (required by the task description)
                FormEditor formEditor = new FormEditor(sourceDoc);

                // Retrieve all form fields from the document
                var formFields = sourceDoc.Form?.Fields;

                // Create a new PDF document that will hold the report
                using (Document reportDoc = new Document())
                {
                    // Add a blank page to the report document
                    Page reportPage = reportDoc.Pages.Add();

                    // Create a table with three columns: Name, Type, Value
                    Table table = new Table
                    {
                        ColumnWidths = "150 100 200",
                        DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                        DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
                    };

                    // Add header row
                    Row header = table.Rows.Add();
                    header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Field Name") } });
                    header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Field Type") } });
                    header.Cells.Add(new Cell { Paragraphs = { new TextFragment("Field Value") } });

                    // Iterate over each form field and populate the table
                    if (formFields != null)
                    {
                        foreach (Field field in formFields)
                        {
                            // Field name (partial name)
                            string fieldName = field.PartialName;

                            // Field type – use the concrete class name (e.g., TextBoxField, CheckBoxField)
                            string fieldType = field.GetType().Name;

                            // Current value of the field (convert to string, handle nulls)
                            string fieldValue = field.Value?.ToString() ?? string.Empty;

                            // Add a new row with the collected information
                            Row row = table.Rows.Add();
                            row.Cells.Add(new Cell { Paragraphs = { new TextFragment(fieldName) } });
                            row.Cells.Add(new Cell { Paragraphs = { new TextFragment(fieldType) } });
                            row.Cells.Add(new Cell { Paragraphs = { new TextFragment(fieldValue) } });
                        }
                    }

                    // Add the table to the page
                    reportPage.Paragraphs.Add(table);

                    // Save the report PDF
                    reportDoc.Save(outputPdfPath);
                }

                // FormEditor does not implement IDisposable; let GC handle it.
            }

            Console.WriteLine($"Form fields report generated: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
