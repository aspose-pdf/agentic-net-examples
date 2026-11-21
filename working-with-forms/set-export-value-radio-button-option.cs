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
        const string fieldName = "myRadioGroup";   // name of the radio button field
        const string optionName = "Option1";       // display name of the specific option to modify
        const string exportCode = "CODE123";       // code required by downstream systems

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the radio button field from the form
            RadioButtonField radioField = doc.Form[fieldName] as RadioButtonField;
            if (radioField == null)
            {
                Console.Error.WriteLine($"Radio button field '{fieldName}' not found.");
                return;
            }

            // Locate the desired option within the radio button field
            Option targetOption = null;
            foreach (Option opt in radioField.Options)
            {
                if (opt.Name == optionName)
                {
                    targetOption = opt;
                    break;
                }
            }

            if (targetOption == null)
            {
                Console.Error.WriteLine($"Option '{optionName}' not found in field '{fieldName}'.");
                return;
            }

            // Set the export value (Value property) that will be emitted when the form is submitted
            targetOption.Value = exportCode;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Export value set and saved to '{outputPath}'.");
    }
}
