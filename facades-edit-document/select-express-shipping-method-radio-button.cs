using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the Form facade with the source PDF
        using (Form form = new Form(inputPath))
        {
            // Fill the radio button field "ShippingMethod" with the option "Express"
            bool success = form.FillField("ShippingMethod", "Express");
            if (!success)
            {
                Console.Error.WriteLine("Failed to fill the 'ShippingMethod' field. Verify the field name and option value.");
            }

            // Save the modified PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"PDF with selected shipping method saved to '{outputPath}'.");
    }
}