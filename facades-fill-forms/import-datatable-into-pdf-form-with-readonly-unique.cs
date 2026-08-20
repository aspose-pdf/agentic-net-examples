using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for Border and BorderStyle

class Program
{
    static void Main()
    {
        // Paths for the template PDF (created on‑the‑fly) and the output PDF
        const string templatePdf = "template.pdf";
        const string outputPdf   = "filled.pdf";

        // ------------------------------------------------------------
        // 1. Create and configure a DataTable that will be imported.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");

        // Column 1: regular editable text field
        DataColumn colFirstName = dataTable.Columns.Add("FirstName", typeof(string));
        colFirstName.ReadOnly = false;   // field can be edited in the PDF form
        colFirstName.Unique   = false;   // duplicate values allowed

        // Column 2: read‑only and unique identifier field
        DataColumn colEmployeeId = dataTable.Columns.Add("EmployeeID", typeof(int));
        colEmployeeId.ReadOnly = true;   // field will be read‑only in the PDF form
        colEmployeeId.Unique   = true;   // each value must be unique

        // Add sample rows (ensure uniqueness for EmployeeID)
        dataTable.Rows.Add("John", 1001);
        dataTable.Rows.Add("Jane", 1002);
        // dataTable.Rows.Add("Bob", 1001); // would throw due to Unique constraint

        // ------------------------------------------------------------
        // 2. Create a PDF template that contains a TextBoxField for each column.
        //    The field's PartialName must match the column name.
        // ------------------------------------------------------------
        if (!File.Exists(templatePdf))
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();
                float yPos = 750f;                     // start near top of page
                const float left = 100f;
                const float width = 300f;
                const float height = 20f;
                const float verticalSpacing = 30f;

                foreach (DataColumn col in dataTable.Columns)
                {
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(left, yPos, left + width, yPos + height);
                    TextBoxField txtField = new TextBoxField(page, rect)
                    {
                        PartialName = col.ColumnName,
                        Value = string.Empty,
                        ReadOnly = col.ReadOnly
                    };
                    // Optional visual styling
                    txtField.Border = new Border(txtField)
                    {
                        Style = BorderStyle.Solid,
                        Width = 1
                    };
                    txtField.Color = Aspose.Pdf.Color.Black;
                    doc.Form.Add(txtField);
                    yPos -= verticalSpacing;
                }

                doc.Save(templatePdf);
            }
        }

        // ------------------------------------------------------------
        // 3. Use AutoFiller to import the DataTable into the PDF form.
        // ------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();
        // Bind the template PDF (the new recommended API)
        autoFiller.BindPdf(templatePdf);
        // Import the configured DataTable – column properties (ReadOnly, Unique) are already respected.
        autoFiller.ImportDataTable(dataTable);
        // Save the filled PDF
        autoFiller.Save(outputPdf);

        Console.WriteLine($"PDF generated successfully: {outputPdf}");
    }
}
