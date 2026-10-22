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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document doc = new Document(inputPath);

        // Define the list items and default selection
        string[] items = new string[] { "Low", "Medium", "High" };
        const string defaultValue = "Medium"; // value to pre‑select

        // Position and size of the field on page 1 (coordinates in points)
        int pageNumber = 1;
        double llx = 100; // lower‑left X
        double lly = 700; // lower‑left Y
        double urx = 200; // upper‑right X
        double ury = 720; // upper‑right Y

        // Ensure the target page exists (add a blank page if necessary)
        while (doc.Pages.Count < pageNumber)
        {
            doc.Pages.Add();
        }

        // Create the rectangle that defines the field bounds
        var rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

        // Create a ComboBox (drop‑down list) field
        ComboBoxField combo = new ComboBoxField(doc.Pages[pageNumber], rect)
        {
            PartialName = "Priority"
        };

        // Populate the combo box with the options (export value, display value)
        foreach (var item in items)
        {
            combo.AddOption(item, item);
        }

        // Set the default selected item by assigning the field's Value
        combo.Value = defaultValue;

        // Add the field to the document's form collection
        doc.Form.Add(combo, pageNumber);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"List field 'Priority' added and saved to '{outputPath}'.");
    }
}
