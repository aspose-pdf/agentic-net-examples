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
        // Paths to the template PDF and the output PDF
        const string templatePdf = "template.pdf";
        const string outputPdf   = "filled.pdf";

        // ------------------------------------------------------------
        // 0. Ensure a template PDF exists – create a minimal one if missing.
        // ------------------------------------------------------------
        if (!File.Exists(templatePdf))
        {
            // Create a new PDF document.
            Document doc = new Document();
            Page page = doc.Pages.Add();

            // Define the form fields that correspond to the DataTable columns.
            string[] fieldNames = { "FirstName", "LastName", "Address", "City", "PostalCode", "Country" };
            float yPos = 700; // starting Y coordinate for the first field
            foreach (string name in fieldNames)
            {
                // Create a TextBoxField for each column using the correct constructor (Page, Rectangle).
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, yPos, 300, yPos + 20);
                TextBoxField txt = new TextBoxField(page, rect);
                txt.PartialName = name;
                doc.Form.Add(txt);
                yPos -= 30; // move down for the next field
            }

            // Save the generated template.
            doc.Save(templatePdf);
        }

        // ------------------------------------------------------------
        // 1. Create a DataTable that will hold the data for the form.
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");

        // Obtain the DataColumnCollection to add custom columns.
        DataColumnCollection columns = dataTable.Columns;

        // Add columns whose names exactly match the field names in the PDF form.
        columns.Add("FirstName",   typeof(string));
        columns.Add("LastName",    typeof(string));
        columns.Add("Address",     typeof(string));
        columns.Add("City",        typeof(string));
        columns.Add("PostalCode",  typeof(string));
        columns.Add("Country",     typeof(string));

        // Example of a custom column that does not exist in the form.
        // It can be used later for calculations or ignored during import.
        columns.Add("Greeting", typeof(string));

        // ------------------------------------------------------------
        // 2. Populate the DataTable with sample data.
        // ------------------------------------------------------------
        dataTable.Rows.Add("John",  "Doe",   "123 Main St", "Springfield", "12345", "USA", "Dear John,");
        dataTable.Rows.Add("Jane",  "Smith", "456 Oak Ave", "Metropolis",  "67890", "USA", "Dear Jane,");

        // ------------------------------------------------------------
        // 3. Use AutoFiller (Aspose.Pdf.Facades) to import the table
        //    into the PDF form and save the result.
        // ------------------------------------------------------------
        using (AutoFiller filler = new AutoFiller())
        {
            // Bind the template PDF file.
            filler.BindPdf(templatePdf);

            // Import the DataTable. Column names must match form field names.
            filler.ImportDataTable(dataTable);

            // Save the filled PDF to the specified output file.
            filler.Save(outputPdf);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdf}'.");
    }
}
