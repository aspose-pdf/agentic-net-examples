using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF form template and the output file
        const string templatePdf = "TemplateForm.pdf";
        const string outputPdf   = "FilledForm.pdf";

        // ------------------------------------------------------------
        // 1. Ensure the template PDF exists – create it inline if missing.
        // ------------------------------------------------------------
        if (!File.Exists(templatePdf))
        {
            // Create a simple PDF with the required form fields.
            Document templateDoc = new Document();
            Page page = templateDoc.Pages.Add();

            // Text fields
            TextBoxField firstName = new TextBoxField(page, new Rectangle(100, 700, 300, 720));
            firstName.PartialName = "FirstName";
            templateDoc.Form.Add(firstName);

            TextBoxField lastName = new TextBoxField(page, new Rectangle(100, 660, 300, 680));
            lastName.PartialName = "LastName";
            templateDoc.Form.Add(lastName);

            TextBoxField email = new TextBoxField(page, new Rectangle(100, 620, 300, 640));
            email.PartialName = "Email";
            templateDoc.Form.Add(email);

            // Checkbox field (correct class name is CheckboxField)
            CheckboxField subscribe = new CheckboxField(page, new Rectangle(100, 580, 120, 600));
            subscribe.PartialName = "Subscribe";
            templateDoc.Form.Add(subscribe);

            // Save the template so AutoFiller can bind to it.
            templateDoc.Save(templatePdf);
        }

        // ------------------------------------------------------------
        // 2. Create a DataTable that will hold the data for the form.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");

        // Use the DataColumnCollection to add columns.
        // Column names must exactly match the field names in the PDF form (case‑sensitive).
        DataColumnCollection columns = dataTable.Columns;
        columns.Add("FirstName", typeof(string));
        columns.Add("LastName",  typeof(string));
        columns.Add("Email",     typeof(string));
        columns.Add("Subscribe", typeof(bool));

        // ------------------------------------------------------------
        // 3. Populate the DataTable with sample rows.
        // ------------------------------------------------------------
        dataTable.Rows.Add("John",  "Doe",  "john.doe@example.com",  true);
        dataTable.Rows.Add("Jane",  "Smith","jane.smith@example.com",false);
        dataTable.Rows.Add("Alice", "Brown","alice.brown@example.com",true);

        // ------------------------------------------------------------
        // 4. Use the AutoFiller facade to bind the template, import the DataTable,
        //    and save the filled PDF.
        // ------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();
        autoFiller.BindPdf(templatePdf);
        autoFiller.ImportDataTable(dataTable);
        autoFiller.Save(outputPdf);
    }
}
