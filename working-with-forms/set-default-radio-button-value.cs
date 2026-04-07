using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "PaymentMethod";
        const string selectedValue = "Credit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF form
            using (Form form = new Form(inputPath))
            {
                // Fill the radio button group with the desired value
                // This works for radio button fields; the value must match one of the option names.
                bool filled = form.FillField(fieldName, selectedValue);
                if (!filled)
                {
                    Console.Error.WriteLine($"Failed to set value for field '{fieldName}'.");
                }

                // Save the updated PDF
                form.Save(outputPath);
            }

            Console.WriteLine($"Radio button '{fieldName}' set to '{selectedValue}' and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}