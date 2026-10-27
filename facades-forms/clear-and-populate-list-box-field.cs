using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the Document API (preferred over FormEditor for form fields)
        Document pdfDoc = new Document(inputPath);

        // Retrieve the combo box (dropdown) field named "CountryList"
        ComboBoxField comboBox = pdfDoc.Form["CountryList"] as ComboBoxField;
        if (comboBox == null)
        {
            Console.Error.WriteLine("Dropdown field 'CountryList' not found or is not a ComboBoxField.");
            return;
        }

        // Remove all existing list items
        comboBox.Options.Clear();

        // Populate new values
        string[] newCountries = { "USA", "Canada", "Mexico", "Germany", "France" };
        foreach (string country in newCountries)
        {
            // AddOption adds a value/display pair; using the same string for both is sufficient here
            comboBox.AddOption(country, country);
        }

        // Save the modified PDF
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
