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
    /// <param name="fieldName">The fully qualified name of the radio button field.</param>
    /// <param name="optionValue">The export value of the option to select (e.g., "Yes", "No").</param>
    static void SelectRadioButton(string inputPdf, string outputPdf, string fieldName, string optionValue)
    {
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the radio button field from the form collection.
            // The Form collection is indexed by the field's full name.
            RadioButtonField radioField = doc.Form[fieldName] as RadioButtonField;

            if (radioField == null)
            {
                Console.Error.WriteLine($"Radio button field '{fieldName}' not found.");
                return;
            }

            // Set the desired option. The Value property accepts the export value of the option.
            // This will automatically update the Selected index internally.
            radioField.Value = optionValue;

            // Optional: ensure the field allows deselection if needed.
            // radioField.NoToggleToOff = false; // uncomment if you want to allow no selection.

            // Save the modified document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Radio button '{fieldName}' set to '{optionValue}'. Saved to '{outputPdf}'.");
    }

    static void Main()
    {
        // Example usage:
        const string inputPath  = "form.pdf";
        const string outputPath = "form_updated.pdf";
        const string fieldName  = "RadioGroup1";   // replace with actual field name in the PDF
        const string option     = "Option2";       // replace with the export value of the desired option

        SelectRadioButton(inputPath, outputPath, fieldName, option);
    }
}