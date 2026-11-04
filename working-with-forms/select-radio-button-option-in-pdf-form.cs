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
        const string radioFieldName = "myRadioGroup"; // name of the radio button field
        const string optionToSelect = "Option2";      // option to be selected (can be passed as a parameter)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the radio button field from the form
                RadioButtonField radioField = doc.Form[radioFieldName] as RadioButtonField;
                if (radioField == null)
                {
                    Console.Error.WriteLine($"Radio button field '{radioFieldName}' not found.");
                }
                else
                {
                    // Get the option by its name. The Options collection returns a RadioButtonOption.
                    var option = radioField.Options[optionToSelect];
                    if (option != null)
                    {
                        // The Selected property expects the option index (int).
                        radioField.Selected = option.Index;
                        Console.WriteLine($"Selected option '{optionToSelect}'.");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Option '{optionToSelect}' not found in radio group '{radioFieldName}'.");
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
