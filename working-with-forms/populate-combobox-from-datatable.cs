using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdfPath = "template.pdf";
        const string outputPdfPath = "filled.pdf";

        // -----------------------------------------------------------------
        // Ensure a template PDF with a ComboBox field exists (self‑contained).
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (var seedDoc = new Document())
            {
                // Add a page to the document.
                var page = seedDoc.Pages.Add();

                // Define the rectangle that will contain the ComboBox.
                var comboRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

                // Create the ComboBox field and give it a partial name that matches the lookup.
                var comboField = new ComboBoxField(page, comboRect)
                {
                    PartialName = "MyComboBox"
                };

                // Add the field to the document's form collection.
                seedDoc.Form.Add(comboField);

                // Save the template for later use.
                seedDoc.Save(inputPdfPath);
            }
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Access the form fields collection.
            Form form = pdfDoc.Form;

            // Retrieve the ComboBox field by its name. The cast may return null, so use a nullable variable.
            ComboBoxField? comboBox = form["MyComboBox"] as ComboBoxField;
            if (comboBox == null)
            {
                Console.Error.WriteLine("ComboBox field not found.");
                return;
            }

            // -----------------------------------------------------------------
            // Sample data source – in‑memory DataTable (replaces SqlClient usage).
            // -----------------------------------------------------------------
            DataTable optionsTable = new DataTable();
            optionsTable.Columns.Add("OptionValue", typeof(string));
            // Add sample rows – in a real scenario these could be filled from any source.
            optionsTable.Rows.Add("Apple");
            optionsTable.Rows.Add("Banana");
            optionsTable.Rows.Add("Cherry");
            optionsTable.Rows.Add("Date");

            // Populate the ComboBox with the values from the DataTable.
            foreach (DataRow row in optionsTable.Rows)
            {
                string optionText = row["OptionValue"].ToString();
                comboBox.AddOption(optionText);
            }

            // Optionally set the first option as selected (options are 1‑based).
            if (comboBox.Options.Count > 0)
                comboBox.Selected = 1;

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"ComboBox populated and saved to '{outputPdfPath}'.");
    }
}
