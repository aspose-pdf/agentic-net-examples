using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Country";
        const string selectedValue = "United States";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Locate the dropdown (ComboBox) field and set its default selected value.
        var combo = doc.Form?.Fields?.FirstOrDefault(f => f.Name == fieldName) as ComboBoxField;
        if (combo != null)
        {
            combo.Value = selectedValue; // sets the default selected item
        }
        else
        {
            Console.Error.WriteLine($"Dropdown field '{fieldName}' not found or is not a ComboBox.");
        }

        // Save the modified PDF.
        doc.Save(outputPath);
        Console.WriteLine($"Dropdown '{fieldName}' set to '{selectedValue}' and saved to '{outputPath}'.");
    }
}
