using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF form template (must contain form fields with matching names)
        const string templatePath = "template.pdf";

        // Directory where filled PDFs will be saved
        const string outputDir = "GeneratedForms";

        // ---------------------------------------------------------------------
        // In‑memory DataTable that mimics a database result set.
        // ---------------------------------------------------------------------
        DataTable data = new DataTable();
        data.Columns.Add("Id", typeof(string));
        data.Columns.Add("Name", typeof(string));
        data.Columns.Add("Address", typeof(string));
        data.Columns.Add("Date", typeof(DateTime));

        data.Rows.Add("001", "Alice Johnson", "123 Main St, Springfield", new DateTime(2023, 5, 12));
        data.Rows.Add("002", "Bob Smith", "456 Oak Ave, Metropolis", new DateTime(2023, 6, 3));
        data.Rows.Add("003", "Carol Lee", "789 Pine Rd, Gotham", new DateTime(2023, 7, 21));

        // Verify that the template exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Iterate over each record and generate a filled PDF
        foreach (DataRow row in data.Rows)
        {
            string id = row["Id"].ToString();
            string name = row["Name"].ToString();
            string address = row["Address"].ToString();
            string date = ((DateTime)row["Date"]).ToString("yyyy-MM-dd");

            // Load a fresh copy of the template for this record
            using (Document pdf = new Document(templatePath))
            {
                // Populate form fields – cast to the concrete field type that exposes the Value property.
                if (pdf.Form["Id"] is TextBoxField idField)               idField.Value = id;
                if (pdf.Form["Name"] is TextBoxField nameField)           nameField.Value = name;
                if (pdf.Form["Address"] is TextBoxField addressField)     addressField.Value = address;
                if (pdf.Form["Date"] is TextBoxField dateField)           dateField.Value = date;

                // Make fields non‑editable in the output PDF
                pdf.Form.Flatten();

                // Save the filled PDF
                string outputPath = Path.Combine(outputDir, $"Form_{id}.pdf");
                pdf.Save(outputPath);
                Console.WriteLine($"Saved: {outputPath}");
            }
        }
    }
}
