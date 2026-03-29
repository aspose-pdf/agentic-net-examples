using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "ShippingMethod";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF form
        Form form = new Form(inputPath);

        // Retrieve the option values for the radio button field
        var optionValues = form.GetButtonOptionValues(fieldName); // returns Dictionary<string,string>

        // Verify that the desired option exists (case‑insensitive)
        bool hasExpress = false;
        foreach (var kvp in optionValues)
        {
            if (kvp.Value.Equals("Express", StringComparison.OrdinalIgnoreCase) ||
                kvp.Key.Equals("Express", StringComparison.OrdinalIgnoreCase))
            {
                hasExpress = true;
                break;
            }
        }

        if (!hasExpress)
        {
            Console.Error.WriteLine($"Option 'Express' not found for field {fieldName}");
            return;
        }

        // Select the desired radio button option by its value (or key)
        bool filled = form.FillField(fieldName, "Express");
        if (!filled)
        {
            Console.Error.WriteLine("Failed to fill the field.");
            return;
        }

        // Save the updated PDF
        form.Save(outputPath);
        Console.WriteLine($"Radio button '{fieldName}' set to 'Express' and saved to '{outputPath}'.");
    }
}
