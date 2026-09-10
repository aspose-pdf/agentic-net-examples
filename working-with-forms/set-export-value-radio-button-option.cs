using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string radioFieldName = "MyRadioGroup";   // name of the radio button field in the PDF
        const string optionExportValue = "CODE123";     // the export value required by downstream systems

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the radio button field by its name
            RadioButtonField radio = form[radioFieldName] as RadioButtonField;
            if (radio == null)
            {
                Console.Error.WriteLine($"Radio button field '{radioFieldName}' not found.");
                return;
            }

            // Ensure the radio button has at least one option
            if (radio.Options.Count == 0)
            {
                Console.Error.WriteLine("Radio button has no options to set export value on.");
                return;
            }

            // Example: set the export value of the first option.
            // Options collection contains Option objects; each Option has a Value property (export value).
            radio.Options[0].Value = optionExportValue;

            // If you need to set a specific option by name, you can locate it:
            // foreach (Option opt in radio.Options)
            // {
            //     if (opt.Name == "DesiredOptionName")
            //     {
            //         opt.Value = optionExportValue;
            //         break;
            //     }
            // }

            // Save the modified PDF (lifecycle rule: use the same Document instance)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Export value set and PDF saved to '{outputPath}'.");
    }
}