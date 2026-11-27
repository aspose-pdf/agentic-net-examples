using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "output.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Retrieve the dropdown field named "dropdown1"
            // It is typically a ComboBoxField; cast accordingly
            ComboBoxField dropdown = doc.Form["dropdown1"] as ComboBoxField;

            if (dropdown == null)
            {
                Console.Error.WriteLine("Dropdown field 'dropdown1' not found or is not a ComboBoxField.");
                return;
            }

            // The selected option is available via the Value property
            string selectedOption = dropdown.Value ?? string.Empty;

            // Write the selected option to a CSV file
            using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
            {
                // CSV header
                writer.WriteLine("FieldName,SelectedValue");
                // CSV data row
                writer.WriteLine($"{dropdown.FullName},{selectedOption}");
            }

            Console.WriteLine($"Selected option written to '{outputCsvPath}'.");
        }
    }
}