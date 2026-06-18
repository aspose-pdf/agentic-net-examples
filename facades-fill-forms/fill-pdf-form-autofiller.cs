using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------------------------------------
            // 1. Create a sample PDF with four text box fields (self‑contained).
            // ------------------------------------------------------------
            using (Document templateDoc = new Document())
            {
                // Add a page (1‑based indexing).
                Page page = templateDoc.Pages.Add();

                // Name field
                Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
                TextBoxField nameField = new TextBoxField(page, nameRect);
                nameField.PartialName = "Name";
                templateDoc.Form.Add(nameField);

                // Email field
                Aspose.Pdf.Rectangle emailRect = new Aspose.Pdf.Rectangle(100, 650, 300, 670);
                TextBoxField emailField = new TextBoxField(page, emailRect);
                emailField.PartialName = "Email";
                templateDoc.Form.Add(emailField);

                // Address field
                Aspose.Pdf.Rectangle addressRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
                TextBoxField addressField = new TextBoxField(page, addressRect);
                addressField.PartialName = "Address";
                templateDoc.Form.Add(addressField);

                // Phone field
                Aspose.Pdf.Rectangle phoneRect = new Aspose.Pdf.Rectangle(100, 550, 300, 570);
                TextBoxField phoneField = new TextBoxField(page, phoneRect);
                phoneField.PartialName = "Phone";
                templateDoc.Form.Add(phoneField);

                // Save the template PDF.
                templateDoc.Save("template.pdf");
            }

            // ------------------------------------------------------------
            // 2. Prepare a DataTable with columns that match the PDF field names.
            // ------------------------------------------------------------
            DataTable dataTable = new DataTable("FormData");
            DataColumnCollection columns = dataTable.Columns;
            columns.Add("Name", typeof(string));
            columns.Add("Email", typeof(string));
            columns.Add("Address", typeof(string));
            columns.Add("Phone", typeof(string));

            DataRow row = dataTable.NewRow();
            row["Name"] = "John Doe";
            row["Email"] = "john.doe@example.com";
            row["Address"] = "123 Main St";
            row["Phone"] = "555-1234";
            dataTable.Rows.Add(row);

            // ------------------------------------------------------------
            // 3. Fill the PDF form using AutoFiller.
            // ------------------------------------------------------------
            AutoFiller autoFiller = new AutoFiller();
            autoFiller.BindPdf("template.pdf"); // Preferred initialization method.
            autoFiller.ImportDataTable(dataTable);
            autoFiller.Save("filled.pdf");

            Console.WriteLine("Form‑filled PDF saved as filled.pdf");
        }
    }
}
