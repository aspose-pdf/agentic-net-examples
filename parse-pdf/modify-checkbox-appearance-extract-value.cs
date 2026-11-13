using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // required for Border and BorderStyle
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";
        const string checkBoxName = "myCheckBox"; // name of the checkbox field in the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the checkbox field by name
            if (doc.Form[checkBoxName] is CheckboxField checkBox)
            {
                // Modify appearance using the correct Aspose.Pdf APIs
                // Set border (requires the parent annotation in the constructor)
                checkBox.Border = new Border(checkBox) { Width = 1, Style = BorderStyle.Solid };
                // Set border colour and background colour via Characteristics (System.Drawing.Color)
                checkBox.Characteristics.Border = System.Drawing.Color.Black;
                checkBox.Characteristics.Background = System.Drawing.Color.LightGray;

                // Optionally set the checked state (true/false)
                checkBox.Checked = true;

                // Extract the current value after modification
                bool isChecked = checkBox.Checked;
                Console.WriteLine($"Checkbox '{checkBoxName}' is {(isChecked ? "checked" : "unchecked")}.");

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.Error.WriteLine($"Checkbox field '{checkBoxName}' not found in the document.");
            }
        }
    }
}
