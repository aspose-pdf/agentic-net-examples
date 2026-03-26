using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "form_report.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        // Load the source PDF that contains the form fields
        using (Document sourceDoc = new Document(inputPath))
        {
            // Initialize the Form facade on the loaded document
            Form formFacade = new Form(sourceDoc);

            // Retrieve all field names present in the form
            IList<string> fieldNames = formFacade.FieldNames;

            // Create a new PDF document that will hold the report
            using (Document reportDoc = new Document())
            {
                // Add a single page to the report document
                Page page = reportDoc.Pages.Add();

                // Create a table with three columns: Name, Type, Value
                Table table = new Table();
                table.ColumnWidths = "150 100 200";

                // Add a header row
                Row headerRow = table.Rows.Add();
                Cell headerCell1 = headerRow.Cells.Add("Field Name");
                Cell headerCell2 = headerRow.Cells.Add("Field Type");
                Cell headerCell3 = headerRow.Cells.Add("Field Value");
                headerCell1.BackgroundColor = Aspose.Pdf.Color.LightGray;
                headerCell2.BackgroundColor = Aspose.Pdf.Color.LightGray;
                headerCell3.BackgroundColor = Aspose.Pdf.Color.LightGray;

                // Populate the table with each form field's details
                foreach (string fieldName in fieldNames)
                {
                    // Get the field type enum and convert it to a string for display
                    string fieldType = formFacade.GetFieldType(fieldName).ToString();
                    string fieldValue = formFacade.GetField(fieldName);

                    Row dataRow = table.Rows.Add();
                    dataRow.Cells.Add(fieldName);
                    dataRow.Cells.Add(fieldType);
                    dataRow.Cells.Add(fieldValue);
                }

                // Add the completed table to the page
                page.Paragraphs.Add(table);

                // Save the report PDF
                reportDoc.Save(outputPath);
            }

            // Close the Form facade (optional, as it implements IDisposable)
            formFacade.Close();
        }

        Console.WriteLine("Form fields report saved to '" + outputPath + "'.");
    }
}
