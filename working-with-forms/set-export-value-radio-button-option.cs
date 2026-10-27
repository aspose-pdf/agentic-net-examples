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
        const string fieldName = "myRadioGroup"; // name of the radio button field
        const int optionIndex = 2;               // zero‑based index of the option to modify
        const string exportCode = "CODE123";    // value required by downstream systems

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
                // Retrieve the radio button field by name
                RadioButtonField radio = doc.Form[fieldName] as RadioButtonField;
                if (radio == null)
                {
                    Console.Error.WriteLine($"Radio button field '{fieldName}' not found.");
                }
                else
                {
                    // Verify the option index is within range
                    if (optionIndex >= 0 && optionIndex < radio.Options.Count)
                    {
                        // Set the export value (Name) of the selected option
                        radio.Options[optionIndex].Name = exportCode;
                        Console.WriteLine($"Export value of option {optionIndex} set to '{exportCode}'.");
                    }
                    else
                    {
                        Console.Error.WriteLine($"Option index {optionIndex} out of range (count: {radio.Options.Count}).");
                    }
                }

                // Save the modified PDF (PDF format by default)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
