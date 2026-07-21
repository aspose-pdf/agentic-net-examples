using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // Ensure the template PDF exists (create it inline if it does not).
        CreateTemplateIfMissing(templatePath);

        // Retrieve combo box items from a database (placeholder implementation)
        List<string> dbValues = GetComboBoxValuesFromDatabase();

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(templatePath))
        {
            // Access the form collection
            Form form = doc.Form;

            // Use the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the combo box (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a ComboBoxField on the specified page and rectangle
            ComboBoxField combo = new ComboBoxField(page, rect)
            {
                Name        = "MyComboBox",
                PartialName = "MyComboBox",
                Editable    = true,                         // Allow user to type custom text
                Color       = Aspose.Pdf.Color.LightGray    // Visual appearance (optional)
            };

            // Add each value retrieved from the database as an option
            foreach (string value in dbValues)
            {
                combo.AddOption(value);
            }

            // Add the combo box field to the document's form
            form.Add(combo);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"ComboBox populated and saved to '{outputPath}'.");
    }

    // Creates a minimal PDF file if the expected template does not exist.
    static void CreateTemplateIfMissing(string path)
    {
        if (!System.IO.File.Exists(path))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // Add a blank page.
                doc.Save(path);
            }
        }
    }

    // Placeholder for actual database access logic
    static List<string> GetComboBoxValuesFromDatabase()
    {
        // Replace this stub with real DB code (e.g., ADO.NET, Dapper, EF)
        return new List<string> { "Option A", "Option B", "Option C" };
    }
}
