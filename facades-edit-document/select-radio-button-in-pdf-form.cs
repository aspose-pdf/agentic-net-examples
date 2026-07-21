using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "ShippingMethod"; // full field name of the radio group
        const string optionValue = "Express";       // the export value of the desired option

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF form
        using (Form form = new Form(inputPath))
        {
            // Fill the radio button field with the specified option
            bool success = form.FillField(fieldName, optionValue);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set field '{fieldName}' to value '{optionValue}'.");
            }

            // Save the updated PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"Radio button '{optionValue}' selected and saved to '{outputPath}'.");
    }
}