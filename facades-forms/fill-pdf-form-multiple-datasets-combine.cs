using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF form template (must contain form fields matching the DataTable column names)
        const string templatePath = "TemplateForm.pdf";

        // Ensure a template exists – create a minimal one if it does not.
        EnsureTemplate(templatePath);

        // Directory to store intermediate filled PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "AutoFillerTemp");
        Directory.CreateDirectory(tempDir);

        // Prepare several independent synthetic DataTables
        List<DataTable> dataSets = new List<DataTable>
        {
            CreateSampleTable(1),
            CreateSampleTable(2),
            CreateSampleTable(3)
        };

        // List to hold paths of the filled PDFs
        List<string> filledPdfPaths = new List<string>();

        // Process each DataTable with a fresh AutoFiller instance
        for (int i = 0; i < dataSets.Count; i++)
        {
            DataTable dt = dataSets[i];
            string filledPath = Path.Combine(tempDir, $"Filled_{i + 1}.pdf");

            // AutoFiller implements IDisposable, so wrap it in a using block
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the template PDF
                filler.BindPdf(templatePath);

                // Import the DataTable – this is the correct method (not FillDataTable)
                filler.ImportDataTable(dt);

                // Save the filled PDF to a temporary file
                filler.Save(filledPath);
            }

            filledPdfPaths.Add(filledPath);
        }

        // Combine all filled PDFs into a single output PDF using PdfFileEditor (Facades API)
        const string combinedOutputPath = "CombinedFilled.pdf";
        PdfFileEditor editor = new PdfFileEditor();

        // Concatenate accepts an array of source file paths and the destination path
        editor.Concatenate(filledPdfPaths.ToArray(), combinedOutputPath);

        // Clean up temporary files
        foreach (string file in filledPdfPaths)
        {
            try { File.Delete(file); } catch { /* ignore cleanup errors */ }
        }
        try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }

        Console.WriteLine($"Combined filled PDF saved to '{combinedOutputPath}'.");
    }

    // Helper method to create a synthetic DataTable with sample data
    static DataTable CreateSampleTable(int setNumber)
    {
        DataTable table = new DataTable("SampleData");
        table.Columns.Add("FirstName", typeof(string));
        table.Columns.Add("LastName", typeof(string));
        table.Columns.Add("Email", typeof(string));

        // Add a few rows; values can be customized per dataset
        for (int i = 1; i <= 3; i++)
        {
            DataRow row = table.NewRow();
            row["FirstName"] = $"First{setNumber}_{i}";
            row["LastName"]  = $"Last{setNumber}_{i}";
            row["Email"]     = $"user{setNumber}_{i}@example.com";
            table.Rows.Add(row);
        }

        return table;
    }

    // Creates a minimal PDF form template if the file does not exist.
    static void EnsureTemplate(string path)
    {
        if (File.Exists(path))
            return;

        // Create a new PDF document with a single page.
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define rectangles for the three text fields.
        var rectFirst = new Rectangle(100, 700, 300, 720);
        var rectLast  = new Rectangle(100, 660, 300, 680);
        var rectEmail = new Rectangle(100, 620, 300, 640);

        // Create text box fields that match the DataTable column names.
        var firstNameField = new TextBoxField(page, rectFirst) { PartialName = "FirstName", Value = "" };
        var lastNameField  = new TextBoxField(page, rectLast)  { PartialName = "LastName",  Value = "" };
        var emailField     = new TextBoxField(page, rectEmail) { PartialName = "Email",     Value = "" };

        // Add fields to the document's form collection (page number is 1‑based).
        doc.Form.Add(firstNameField, 1);
        doc.Form.Add(lastNameField, 1);
        doc.Form.Add(emailField, 1);

        // Save the template.
        doc.Save(path);
    }
}
