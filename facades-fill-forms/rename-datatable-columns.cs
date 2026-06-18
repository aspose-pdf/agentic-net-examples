using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

namespace RenameDataTableColumnsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF with three text fields.
            using (Document templateDoc = new Document())
            {
                templateDoc.Pages.Add();

                // FirstName field
                TextBoxField firstNameField = new TextBoxField(
                    templateDoc.Pages[1],
                    new Aspose.Pdf.Rectangle(100, 700, 300, 730));
                firstNameField.PartialName = "FirstName";
                templateDoc.Form.Add(firstNameField);

                // LastName field
                TextBoxField lastNameField = new TextBoxField(
                    templateDoc.Pages[1],
                    new Aspose.Pdf.Rectangle(100, 650, 300, 680));
                lastNameField.PartialName = "LastName";
                templateDoc.Form.Add(lastNameField);

                // Email field
                TextBoxField emailField = new TextBoxField(
                    templateDoc.Pages[1],
                    new Aspose.Pdf.Rectangle(100, 600, 300, 630));
                emailField.PartialName = "Email";
                templateDoc.Form.Add(emailField);

                templateDoc.Save("template.pdf");
            }

            // Step 2: Build a DataTable whose column names do NOT match the PDF field names.
            DataTable sourceTable = new DataTable("People");
            sourceTable.Columns.Add("FName", typeof(string));
            sourceTable.Columns.Add("LName", typeof(string));
            sourceTable.Columns.Add("Mail", typeof(string));

            sourceTable.Rows.Add("John", "Doe", "john.doe@example.com");
            sourceTable.Rows.Add("Jane", "Smith", "jane.smith@example.com");

            // Step 3: Rename the DataTable columns to match the PDF field identifiers.
            sourceTable.Columns["FName"].ColumnName = "FirstName";
            sourceTable.Columns["LName"].ColumnName = "LastName";
            sourceTable.Columns["Mail"].ColumnName = "Email";

            // Step 4: Use AutoFiller to merge the data into the PDF.
            using (AutoFiller filler = new AutoFiller())
            {
                filler.InputFileName = "template.pdf";
                filler.ImportDataTable(sourceTable);
                filler.Save("filled.pdf");
            }
        }
    }
}
