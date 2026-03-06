using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // Path to the source PDF containing the checkbox
        const string outputPdf = "output.pdf";  // Path where the modified PDF will be saved
        const string checkBoxName = "MyCheckBox"; // Fully qualified name of the checkbox field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF form using the Facades Form class
        using (Form form = new Form(inputPdf))
        {
            // Set the checkbox value to true (checked). Use false to uncheck.
            bool success = form.FillField(checkBoxName, true);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set value for checkbox '{checkBoxName}'.");
                return;
            }

            // Save the updated PDF
            form.Save(outputPdf);
        }

        Console.WriteLine($"Checkbox '{checkBoxName}' set and saved to '{outputPdf}'.");
    }
}