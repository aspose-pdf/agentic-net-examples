using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Selects a radio button option in an existing PDF form.
    // Parameters:
    //   inputPdf   - path to the source PDF containing the radio button field.
    //   outputPdf  - path where the modified PDF will be saved.
    //   fieldName  - the fully qualified name of the radio button field.
    //   optionIndex- 1‑based index of the option to select (as defined in the PDF).
    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <outputPdf> <fieldName> <optionIndex>");
            return;
        }

        string inputPdf   = args[0];
        string outputPdf  = args[1];
        string fieldName  = args[2];
        if (!int.TryParse(args[3], out int optionIndex) || optionIndex < 1)
        {
            Console.Error.WriteLine("Option index must be a positive integer (1‑based).");
            return;
        }

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the radio button field from the form collection
            RadioButtonField radioField = doc.Form[fieldName] as RadioButtonField;

            if (radioField == null)
            {
                Console.Error.WriteLine($"Radio button field '{fieldName}' not found.");
                return;
            }

            // Ensure the requested option index is within the available range
            if (optionIndex > radioField.Count)
            {
                Console.Error.WriteLine($"Option index {optionIndex} exceeds the number of options ({radioField.Count}).");
                return;
            }

            // Select the desired option (property is 1‑based)
            radioField.Selected = optionIndex;

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Radio button '{fieldName}' set to option {optionIndex} and saved to '{outputPdf}'.");
    }
}