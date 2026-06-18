using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace GeneratePdfWithCustomNames
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple PDF template with a single text box field.
            using (Document template = new Document())
            {
                Page page = template.Pages.Add();
                // Define the rectangle where the field will appear (left, bottom, right, top).
                Rectangle fieldRect = new Rectangle(100, 700, 300, 750);
                TextBoxField nameField = new TextBoxField(template.Pages[1], fieldRect);
                nameField.PartialName = "NameField";
                nameField.Value = string.Empty;
                template.Form.Add(nameField);
                template.Save("template.pdf");
            }

            // Step 2: Prepare a DataTable with custom file names.
            DataTable table = new DataTable("Names");
            DataColumn nameColumn = new DataColumn("Name", typeof(string));
            DataColumn fileNameColumn = new DataColumn("FileName", typeof(string));
            table.Columns.Add(nameColumn);
            table.Columns.Add(fileNameColumn);

            // Add up to four rows (evaluation mode limit).
            DataRow row1 = table.NewRow();
            row1["Name"] = "Alice Johnson";
            row1["FileName"] = "alice_report";
            table.Rows.Add(row1);

            DataRow row2 = table.NewRow();
            row2["Name"] = "Bob Smith";
            row2["FileName"] = "bob_summary";
            table.Rows.Add(row2);

            DataRow row3 = table.NewRow();
            row3["Name"] = "Carol Lee";
            row3["FileName"] = "carol_overview";
            table.Rows.Add(row3);

            DataRow row4 = table.NewRow();
            row4["Name"] = "David Brown";
            row4["FileName"] = "david_details";
            table.Rows.Add(row4);

            // Step 3: Generate a PDF for each row using the custom file name.
            foreach (DataRow dataRow in table.Rows)
            {
                string customFileName = dataRow["FileName"].ToString() + ".pdf";
                using (Document doc = new Document("template.pdf"))
                {
                    TextBoxField field = (TextBoxField)doc.Form["NameField"];
                    field.Value = dataRow["Name"].ToString();
                    doc.Save(customFileName);
                }
            }
        }
    }
}
