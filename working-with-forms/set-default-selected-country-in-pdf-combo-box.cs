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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Retrieve the 'Country' combo box field from the AcroForm.
        // The indexer returns a WidgetAnnotation, so cast it to Field first.
        Field field = doc.Form["Country"] as Field;
        if (field is ComboBoxField comboBox)
        {
            // Directly set the combo box's value to the display text "United States".
            // This selects the corresponding option in the dropdown.
            comboBox.Value = "United States";
        }
        else
        {
            Console.Error.WriteLine("Field 'Country' is not a combo box or does not exist.");
        }

        // Save the updated PDF
        doc.Save(outputPath);
        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
