using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    /// <summary>
    /// Loads a PDF, selects a radio button option, and saves the result.
    /// </summary>
    /// <param name="inputPdf">Path to the source PDF.</param>
    /// <param name="outputPdf">Path where the modified PDF will be saved.</param>
    /// <param name="fieldName">Fully qualified name of the radio button field.</param>
    /// <param name="optionName">The option value to select.</param>
    static void SelectRadioButton(string inputPdf, string outputPdf, string fieldName, string optionName)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the radio button field from the form collection
            // The Form property holds all interactive form fields.
            RadioButtonField radioField = doc.Form[fieldName] as RadioButtonField;

            if (radioField == null)
            {
                Console.Error.WriteLine($"Radio button field '{fieldName}' not found.");
                return;
            }

            // Set the desired option.
            // The Value property accepts the export value of the option.
            // Alternatively, you could set radioField.Selected (1‑based index).
            radioField.Value = optionName;

            // Save the modified document (lifecycle rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Radio button '{fieldName}' set to '{optionName}'. Saved to '{outputPdf}'.");
    }

    static void Main()
    {
        // Example usage:
        const string inputPath  = "form.pdf";
        const string outputPath = "form_updated.pdf";
        const string fieldName  = "myRadioGroup";   // replace with actual field name
        const string optionName = "Option2";        // replace with the option you want to select

        SelectRadioButton(inputPath, outputPath, fieldName, optionName);
    }
}