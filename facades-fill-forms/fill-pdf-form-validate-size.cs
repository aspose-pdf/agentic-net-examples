using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // AutoFiller resides here
using Aspose.Pdf.Forms;    // Form field classes
using Aspose.Pdf.Text;     // FontRepository and Font classes

class Program
{
    // Predefined maximum file size in bytes (example: 5 MB)
    const long MaxFileSizeBytes = 5L * 1024 * 1024;

    static void Main()
    {
        const string templatePath = "template.pdf";   // PDF with form fields
        const string outputPath   = "filled_output.pdf";

        // ---------------------------------------------------------------------
        // Ensure a template PDF exists. If it does not, create a minimal one with
        // the required form fields (Name, Address, City, Zip). This removes the
        // runtime FileNotFoundException that caused the original build error.
        // ---------------------------------------------------------------------
        EnsureTemplateExists(templatePath);

        // Build a sample DataTable matching the form field names
        DataTable data = new DataTable("FormData");
        data.Columns.Add("Name",    typeof(string));
        data.Columns.Add("Address", typeof(string));
        data.Columns.Add("City",    typeof(string));
        data.Columns.Add("Zip",     typeof(string));

        // Add rows – in a real scenario this would come from a database
        data.Rows.Add("John Doe",   "123 Main St",   "Springfield", "12345");
        data.Rows.Add("Jane Smith", "456 Oak Ave",   "Shelbyville", "67890");
        // ... add as many rows as needed ...

        // Use AutoFiller to bind the template, import the data and save the result
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePath);          // input template PDF
            filler.ImportDataTable(data);          // fill all rows
            filler.Save(outputPath);               // generate merged PDF
        }

        // Validate the generated PDF size
        FileInfo info = new FileInfo(outputPath);
        if (info.Length > MaxFileSizeBytes)
        {
            Console.Error.WriteLine(
                $"Error: Generated PDF size {info.Length} bytes exceeds the limit of {MaxFileSizeBytes} bytes.");
        }
        else
        {
            Console.WriteLine(
                $"Success: PDF generated at '{outputPath}' with size {info.Length} bytes (within limit).");
        }
    }

    /// <summary>
    /// Creates a very simple PDF containing the form fields required by the sample
    /// DataTable if the file does not already exist.
    /// </summary>
    private static void EnsureTemplateExists(string path)
    {
        if (File.Exists(path))
            return;

        // Create a new PDF document
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Helper to add a TextBoxField at a given position
        void AddTextBox(string fieldName, double llx, double lly, double urx, double ury)
        {
            var rect = new Rectangle(llx, lly, urx, ury);
            var txtField = new TextBoxField(page, rect)
            {
                PartialName = fieldName,
                Value = string.Empty
                // Font and FontSize properties are not available in recent Aspose.Pdf versions.
                // The default appearance (font, size, color) will be used.
            };
            doc.Form.Add(txtField);
        }

        // Add fields that match the DataTable column names
        AddTextBox("Name",    100, 700, 300, 720);
        AddTextBox("Address", 100, 660, 300, 680);
        AddTextBox("City",    100, 620, 300, 640);
        AddTextBox("Zip",     100, 580, 300, 600);

        // Save the template for later use
        doc.Save(path);
    }
}
