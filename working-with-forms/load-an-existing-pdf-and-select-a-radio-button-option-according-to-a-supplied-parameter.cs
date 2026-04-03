using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF containing a radio button group
        const string inputPdf  = "input.pdf";
        // Output PDF with the selected option
        const string outputPdf = "output.pdf";
        // Name of the radio button field (as defined in the PDF form)
        const string fieldName = "RadioGroup1";
        // Desired option to select (must match one of the option names in the field)
        const string optionToSelect = "OptionB";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF, modify the radio button, and save
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the field by name and cast to RadioButtonField
            RadioButtonField radioField = doc.Form[fieldName] as RadioButtonField;
            if (radioField == null)
            {
                Console.Error.WriteLine($"Radio button field '{fieldName}' not found.");
                return;
            }

            // Set the selected option by its export value (option name)
            radioField.Value = optionToSelect;

            // Alternatively, you could set the index (1‑based) via radioField.Selected = index;

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Radio button '{fieldName}' set to '{optionToSelect}'. Saved to '{outputPdf}'.");
    }
}