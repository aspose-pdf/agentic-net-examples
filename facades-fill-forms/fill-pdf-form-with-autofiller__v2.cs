using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for Border and BorderStyle

class Program
{
    // Paths for the template and the output PDF.
    private const string TemplatePath = "template.pdf";
    private const string OutputPath   = "filled_output.pdf";

    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Prepare sample data – a DataTable whose column names match the form
        //    field names that will be created in the template PDF.
        // ---------------------------------------------------------------------
        DataTable data = new DataTable("FormData");
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName",  typeof(string));
        data.Columns.Add("Email",     typeof(string));

        DataRow row = data.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Email"]     = "john.doe@example.com";
        data.Rows.Add(row);

        // ---------------------------------------------------------------------
        // 2. Ensure the PDF template exists. If it does not, create it on‑the‑fly
        //    with TextBoxField objects whose PartialName matches the DataTable
        //    column names. This satisfies the sandbox requirement of having no
        //    external files.
        // ---------------------------------------------------------------------
        if (!File.Exists(TemplatePath))
        {
            CreatePdfTemplate(data, TemplatePath);
        }

        // ---------------------------------------------------------------------
        // 3. Fill the template using AutoFiller. The using block guarantees that
        //    AutoFiller.Dispose() (which releases unmanaged resources) is called
        //    after the PDF is saved.
        // ---------------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            autoFiller.BindPdf(TemplatePath);
            autoFiller.ImportDataTable(data);
            autoFiller.Save(OutputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{OutputPath}'.");
    }

    /// <summary>
    /// Creates a simple PDF template containing a TextBoxField for each column in the
    /// supplied DataTable. The fields are positioned vertically on the first page.
    /// </summary>
    private static void CreatePdfTemplate(DataTable schemaTable, string path)
    {
        // Create a new empty PDF document.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Layout parameters for the fields.
            const float left = 100f;
            const float width = 300f;
            const float height = 20f;
            const float verticalSpacing = 30f;
            float yPos = 750f; // Start near the top of the page.

            foreach (DataColumn column in schemaTable.Columns)
            {
                // Define the rectangle for the field.
                Rectangle rect = new Rectangle(left, yPos, left + width, yPos + height);

                // Create the TextBoxField.
                TextBoxField txtField = new TextBoxField(page, rect);
                txtField.PartialName = column.ColumnName;
                txtField.Value = string.Empty;
                txtField.Color = Color.Black;

                // Set the border using the correct Aspose.Pdf.Annotations types.
                txtField.Border = new Border(txtField)
                {
                    Style = BorderStyle.Solid,
                    Width = 1
                };

                // Add the field to the document's form collection.
                doc.Form.Add(txtField);

                // Move down for the next field.
                yPos -= verticalSpacing;
            }

            // Save the template so that AutoFiller can bind to it later.
            doc.Save(path);
        }
    }
}
