using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form_input.pdf";
        const string outputPdfPath = "form_report.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF that contains the form fields
        using (Aspose.Pdf.Document sourceDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Wrap the document with the Form facade to access form data
            Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form(sourceDoc);

            // Retrieve all field names
            string[] fieldNames = formFacade.FieldNames;

            // Create a new PDF document for the report
            using (Aspose.Pdf.Document reportDoc = new Aspose.Pdf.Document())
            {
                // Add a page to the report
                Aspose.Pdf.Page page = reportDoc.Pages.Add();

                // Create a table with three columns: Name, Type, Value
                Aspose.Pdf.Table table = new Aspose.Pdf.Table
                {
                    ColumnWidths = "150 100 200", // adjust widths as needed
                    Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 1f, Aspose.Pdf.Color.Black)
                };

                // Header row
                Row header = table.Rows.Add();
                AddCell(header, "Field Name", true);
                AddCell(header, "Field Type", true);
                AddCell(header, "Field Value", true);

                // Data rows
                foreach (string fieldName in fieldNames)
                {
                    // Get field type and value
                    string fieldType = formFacade.GetFieldType(fieldName).ToString();
                    object rawValue = formFacade.GetField(fieldName);
                    string fieldValue = rawValue != null ? rawValue.ToString() : string.Empty;

                    Row row = table.Rows.Add();
                    AddCell(row, fieldName, false);
                    AddCell(row, fieldType, false);
                    AddCell(row, fieldValue, false);
                }

                // Add the table to the page
                page.Paragraphs.Add(table);

                // Save the report
                reportDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Form report saved to '{outputPdfPath}'.");
        }
    }

    // Helper method to add a cell with optional header styling
    private static void AddCell(Row row, string text, bool isHeader)
    {
        Cell cell = row.Cells.Add();
        TextFragment tf = new TextFragment(text);
        if (isHeader)
        {
            tf.TextState.Font = FontRepository.FindFont("Helvetica-Bold");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.White;
            cell.BackgroundColor = Aspose.Pdf.Color.Gray;
        }
        else
        {
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 10;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        }
        cell.Paragraphs.Add(tf);
        cell.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, 0.5f, Aspose.Pdf.Color.LightGray);
    }
}