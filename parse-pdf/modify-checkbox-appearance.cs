using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // retained for potential future drawing needs

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Assume the first form field is a checkbox; adjust index as needed
            CheckboxField checkBox = doc.Form.Fields[0] as CheckboxField;
            if (checkBox == null)
            {
                Console.Error.WriteLine("No checkbox field found on the first position.");
                return;
            }

            // ----- Modify appearance using core APIs -----
            // Set the visual style of the check mark (e.g., a check symbol)
            checkBox.Style = BoxStyle.Check;

            // Set the color of the check mark
            checkBox.Color = Aspose.Pdf.Color.Red;

            // Define a border around the checkbox
            // Border requires the parent annotation (the checkbox itself) and does NOT expose a Color property.
            // Border color is controlled by the annotation's own Color property, which we already set for the check mark.
            checkBox.Border = new Border(checkBox)
            {
                Width = 1 // 1 point border width
            };

            // Optionally set an export value (value written when the form is submitted)
            checkBox.ExportValue = "Yes";

            // ----- Extract the current value of the checkbox -----
            // The Checked property returns true if the box is marked, false otherwise
            bool isChecked = checkBox.Checked;

            Console.WriteLine($"Checkbox '{checkBox.Name}' is checked: {isChecked}");

            // Save the modified document
            doc.Save(outputPath);
            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
    }
}
