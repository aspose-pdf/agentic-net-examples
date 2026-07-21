using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the template PDF and the output filled PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // ---------------------------------------------------------------------
        // Create a minimal PDF with the required form fields so the sandbox has
        // a file to open. This satisfies the "hardcoded-input-file-generate-inline-first"
        // rule.
        // ---------------------------------------------------------------------
        using (Document templateDoc = new Document())
        {
            // Add a single page
            Page page = templateDoc.Pages.Add();

            // Create a text box field for "Name"
            TextBoxField nameField = new TextBoxField(
                templateDoc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 700, 300, 720) // left, bottom, right, top
            );
            nameField.PartialName = "Name";
            templateDoc.Form.Add(nameField);

            // Create a text box field for "Address"
            TextBoxField addressField = new TextBoxField(
                templateDoc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 650, 300, 670)
            );
            addressField.PartialName = "Address";
            templateDoc.Form.Add(addressField);

            // Save the template PDF that will be used by AutoFiller
            templateDoc.Save(templatePath);
        }

        // Create a DataTable whose column names match the form field names in the template
        DataTable data = new DataTable();
        data.Columns.Add("Name",    typeof(string));
        data.Columns.Add("Address", typeof(string));
        data.Rows.Add("John Doe", "123 Main St, Anytown");

        // AutoFiller implements IDisposable – using ensures Dispose (or Close) is called
        using (AutoFiller filler = new AutoFiller())
        {
            // Bind the template PDF to the filler
            filler.BindPdf(templatePath);

            // Import the data to be merged into the form fields
            filler.ImportDataTable(data);

            // Save the filled PDF (single merged output)
            filler.Save(outputPath);
            // Dispose is invoked automatically at the end of the using block
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}
