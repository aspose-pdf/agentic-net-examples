using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "Country";
        const string selectedValue = "United States";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPdf);

        // Retrieve the ComboBox field from the document's form collection.
        if (doc.Form[fieldName] is ComboBoxField comboBox)
        {
            // Ensure the desired value exists in the list of options.
            bool valueExists = false;
            foreach (Option opt in comboBox.Options)
            {
                if (opt.Value == selectedValue)
                {
                    valueExists = true;
                    break;
                }
            }

            if (!valueExists)
            {
                // Add a new option where both the display text and the value are the same.
                // Use the AddOption method instead of the non‑existent Option constructor.
                comboBox.AddOption(selectedValue);
            }

            // Set the default selected value.
            comboBox.Value = selectedValue;
        }
        else
        {
            Console.Error.WriteLine($"ComboBox field '{fieldName}' not found.");
            return;
        }

        // Save the modified PDF.
        doc.Save(outputPdf);

        Console.WriteLine($"Dropdown '{fieldName}' default set to '{selectedValue}'. Saved as '{outputPdf}'.");
    }
}
