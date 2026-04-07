using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the checkbox field
        const string inputPath = "input.pdf";
        // Output PDF with the checkbox set to checked
        const string outputPath = "output.pdf";
        // Full name of the checkbox field (must match the field's full name in the PDF)
        const string checkboxFieldName = "CheckBox1";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF form using the Facades Form class
        Form form = new Form(inputPath);

        // Fill the checkbox field with a checked value (true)
        // Returns true if the field was found and is a checkbox
        bool success = form.FillField(checkboxFieldName, true);
        if (!success)
        {
            Console.Error.WriteLine($"Failed to set checkbox. Field '{checkboxFieldName}' may not exist or is not a checkbox.");
        }

        // Save the modified PDF to the specified output path
        form.Save(outputPath);
        // Close the facade to release resources
        form.Close();

        Console.WriteLine($"Checkbox '{checkboxFieldName}' set to checked and saved to '{outputPath}'.");
    }
}