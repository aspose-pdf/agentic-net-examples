using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputPdf = "form_filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Retrieve combo box items from a database (placeholder implementation)
        List<string> comboItems = GetComboBoxItemsFromDatabase();

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access the AcroForm
            Form form = doc.Form;

            // Locate the combo box field by its name (e.g., "myCombo")
            ComboBoxField combo = form["myCombo"] as ComboBoxField;
            if (combo == null)
            {
                Console.Error.WriteLine("Combo box field 'myCombo' not found.");
                return;
            }

            // Remove any existing options
            var existingOptionNames = new List<string>();
            foreach (Option opt in combo.Options)
                existingOptionNames.Add(opt.Name);
            foreach (string name in existingOptionNames)
                combo.DeleteOption(name);

            // Populate the combo box with items retrieved from the database
            foreach (string item in comboItems)
                combo.AddOption(item); // export value == display name

            // Optionally set the default selected value (1‑based index)
            if (comboItems.Count > 0)
                combo.Selected = 1; // selects the first added option

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Combo box items set and saved to '{outputPdf}'.");
    }

    // Placeholder for actual database retrieval logic
    static List<string> GetComboBoxItemsFromDatabase()
    {
        // In a real scenario, connect to the database and fetch values.
        // Here we return a static list for demonstration purposes.
        return new List<string> { "Option A", "Option B", "Option C" };
    }
}