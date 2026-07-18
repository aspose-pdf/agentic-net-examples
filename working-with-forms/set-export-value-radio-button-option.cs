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

        // Name of the radio button field in the PDF form
        const string radioFieldName = "MyRadioGroup";

        // Name of the option whose export value we want to change
        const string optionName = "Option1";

        // The code that downstream systems expect
        const string exportValue = "CODE123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Retrieve the radio button field by its full name
            RadioButtonField radio = form[radioFieldName] as RadioButtonField;
            if (radio == null)
            {
                Console.Error.WriteLine($"Radio button field '{radioFieldName}' not found.");
                return;
            }

            // Find the specific option and set its export value
            foreach (Option opt in radio.Options)
            {
                if (opt.Name == optionName)
                {
                    opt.Value = exportValue; // Export value used when the form is submitted
                    break;
                }
            }

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Export value set and saved to '{outputPath}'.");
    }
}