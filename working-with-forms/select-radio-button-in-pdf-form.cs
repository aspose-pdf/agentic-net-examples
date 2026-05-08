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
    /// <param name="optionName">Export value of the option to select.</param>
    static void SelectRadioButton(string inputPdf, string outputPdf, string fieldName, string optionName)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to a Forms.Field.
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field in the PDF.");
                return;
            }

            // Ensure the field is a RadioButtonField.
            if (field is RadioButtonField radioButton)
            {
                // Set the value to the desired option name.
                // The option name must match one of the defined export values.
                radioButton.Value = optionName;

                // Optionally, you can control the NoToggleToOff behavior:
                // radioButton.NoToggleToOff = true; // keep exactly one option selected
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a RadioButtonField.");
                return;
            }

            // Save the modified document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Radio button '{fieldName}' set to '{optionName}'. Saved to '{outputPdf}'.");
    }

    static void Main()
    {
        // Example usage:
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string radioField = "myRadioGroup";   // replace with actual field name
        const string option     = "Option2";        // replace with the desired option value

        SelectRadioButton(inputPath, outputPath, radioField, option);
    }
}
