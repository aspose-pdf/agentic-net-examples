using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the input PDF form and the output PDF
        const string inputPdfPath = "form.pdf";
        const string outputPdfPath = "form_filled.pdf";

        // Retrieve the list of items for the combo box from a database (simulated here)
        List<string> comboBoxItems = GetComboBoxValuesFromDatabase();

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Access the AcroForm attached to the document
            Form acroForm = pdfDocument.Form;

            // Locate the combo box field by its name (replace "MyComboBox" with the actual field name)
            ComboBoxField comboBox = acroForm["MyComboBox"] as ComboBoxField;

            if (comboBox != null)
            {
                // NOTE: Options collection is read‑only; clear existing entries using DeleteOption.
                // This loop removes all current options safely.
                while (comboBox.Options.Count > 0)
                {
                    // Each option is an Option object; its Name property holds the display value.
                    // DeleteOption expects the option's name (string).
                    comboBox.DeleteOption(comboBox.Options[0].Name);
                }

                // Populate the combo box with items retrieved from the database using AddOption.
                foreach (string item in comboBoxItems)
                {
                    comboBox.AddOption(item);
                }

                // Optionally set the first item as the selected value.
                if (comboBox.Options.Count > 0)
                {
                    // The Selected property (int) represents the zero‑based index of the chosen option.
                    comboBox.Selected = 0;
                }
            }
            else
            {
                Console.Error.WriteLine("Combo box field 'MyComboBox' not found in the PDF form.");
            }

            // Save the modified PDF to the output path
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Combo box items have been set and saved to '{outputPdfPath}'.");
    }

    // Simulated database call – replace with actual data access logic
    static List<string> GetComboBoxValuesFromDatabase()
    {
        // Example values; in a real scenario, fetch these from a database
        return new List<string>
        {
            "Option A",
            "Option B",
            "Option C",
            "Option D"
        };
    }
}
