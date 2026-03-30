using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        string templatePath = "template.pdf";
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine("Template PDF not found: " + templatePath);
            return;
        }

        DataTable dataTable = new DataTable("Customers");
        dataTable.Columns.Add("CustomerID", typeof(string));
        dataTable.Columns.Add("CompanyName", typeof(string));
        dataTable.Columns.Add("ContactName", typeof(string));
        dataTable.Columns.Add("Address", typeof(string));

        DataRow row1 = dataTable.NewRow();
        row1["CustomerID"] = "C001";
        row1["CompanyName"] = "Acme Corp";
        row1["ContactName"] = "John Doe";
        row1["Address"] = "123 Main St";
        dataTable.Rows.Add(row1);

        DataRow row2 = dataTable.NewRow();
        row2["CustomerID"] = "C002";
        row2["CompanyName"] = "Beta Ltd";
        row2["ContactName"] = "Jane Smith";
        row2["Address"] = "456 Oak Ave";
        dataTable.Rows.Add(row2);

        foreach (DataRow dataRow in dataTable.Rows)
        {
            using (Document document = new Document(templatePath))
            {
                // Iterate over the actual Field objects in the form
                foreach (Field field in document.Form.Fields)
                {
                    string fieldName = field.PartialName; // the name of the form field
                    if (dataRow.Table.Columns.Contains(fieldName))
                    {
                        // Handle the most common field types. Extend as needed.
                        if (field is TextBoxField txtField)
                        {
                            txtField.Value = dataRow[fieldName].ToString();
                        }
                        // NOTE: CheckBoxField handling was removed because the class
                        // is not available in the referenced Aspose.PDF version.
                        // If you need checkbox support, ensure the correct Aspose.PDF
                        // package version is referenced and re‑add the block below:
                        // else if (field is CheckBoxField chkField)
                        // {
                        //     chkField.Checked = dataRow[fieldName].ToString()
                        //         .Equals("true", StringComparison.OrdinalIgnoreCase);
                        // }
                        else if (field is ComboBoxField comboField)
                        {
                            comboField.Value = dataRow[fieldName].ToString();
                        }
                        // Add other field types (RadioButtonField, ListBoxField, etc.) as required.
                    }
                }

                // Build a custom file name using the CustomerID and CompanyName columns.
                string outputFileName = dataRow["CustomerID"].ToString() + "_" +
                                        dataRow["CompanyName"].ToString().Replace(" ", "_") + ".pdf";
                document.Save(outputFileName);
                Console.WriteLine("Saved: " + outputFileName);
            }
        }
    }
}
