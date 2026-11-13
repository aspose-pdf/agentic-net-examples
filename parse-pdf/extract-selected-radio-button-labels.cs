using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Process only radio button fields
                if (field is RadioButtonField radio)
                {
                    // The export value of the selected radio button in this group (1‑based index as string)
                    string selectedValue = radio.Value;

                    // Find the option whose Value matches the selected value
                    string selectedLabel = null;
                    foreach (Option option in radio.Options)
                    {
                        if (option.Value == selectedValue)
                        {
                            // Name holds the visible label for the option
                            selectedLabel = option.Name;
                            break;
                        }
                    }

                    // Output the mapping of field name, selected value, and its label
                    Console.WriteLine($"Radio field '{radio.Name}': Value = '{selectedValue}', Label = '{selectedLabel ?? "None"}'");
                }
            }
        }
    }
}
