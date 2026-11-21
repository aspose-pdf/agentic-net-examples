using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_filled.pdf";
        const string fieldName  = "ShippingMethod"; // AcroForm radio button group name
        const string optionValue = "Express";       // Export value of the desired option

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the Form facade to fill form field values. FormEditor does not provide SetFieldValue.
        using (Form form = new Form())
        {
            // Load the existing PDF
            form.BindPdf(inputPath);

            // Fill the radio button group with the desired export value ("Express")
            form.FillField(fieldName, optionValue);

            // Save the updated PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"Radio button '{fieldName}' set to '{optionValue}'. Saved to '{outputPath}'.");
    }
}
