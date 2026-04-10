using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "ShippingMethod";
        const string fieldValue = "Express";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF form using the Form facade
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(inputPath))
        {
            // Fill the radio button field with the desired option value
            bool success = form.FillField(fieldName, fieldValue);
            if (!success)
            {
                Console.Error.WriteLine($"Failed to set field '{fieldName}' to '{fieldValue}'.");
            }

            // Save the updated PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"Radio button '{fieldName}' set to '{fieldValue}' and saved to '{outputPath}'.");
    }
}