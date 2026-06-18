using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with a single text box field named "Amount"
        using (Document templateDoc = new Document())
        {
            // Add a page
            Page page = templateDoc.Pages.Add();

            // Define the rectangle for the text box field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create the text box field and assign a name
            TextBoxField amountField = new TextBoxField(page, fieldRect);
            amountField.PartialName = "Amount";

            // Add the field to the form on page 1 (Aspose.Pdf uses 1‑based indexing)
            templateDoc.Form.Add(amountField, 1);

            // Save the template PDF (output path must be a simple file name)
            templateDoc.Save("template.pdf");
        }

        // Step 2: Prepare a DataTable with numeric values and format them as strings
        DataTable data = new DataTable("Values");
        // Column name must match the field name in the PDF (case‑sensitive)
        data.Columns.Add("Amount", typeof(string));

        // Add a few rows (evaluation mode limits collections to 4 elements)
        DataRow row1 = data.NewRow();
        double rawValue1 = 1234.5678;
        row1["Amount"] = rawValue1.ToString("N2"); // format with two decimal places
        data.Rows.Add(row1);

        DataRow row2 = data.NewRow();
        double rawValue2 = 9.5;
        row2["Amount"] = rawValue2.ToString("F2"); // format with exactly two decimal places
        data.Rows.Add(row2);

        // Step 3: Use AutoFiller to merge the data into the template PDF
        using (AutoFiller filler = new AutoFiller())
        {
            // Bind the template PDF (obsolete InputFileName property is avoided)
            filler.BindPdf("template.pdf");

            // Import the formatted DataTable – column names must match field names
            filler.ImportDataTable(data);

            // Save the resulting PDF
            filler.Save("output.pdf");
        }
    }
}