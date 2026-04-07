using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "order_form.pdf";
        const string outputPath = "order_form_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Form form = new Form(inputPath))
        {
            // Fill the radio button group "ShippingMethod" with the option "Express"
            bool success = form.FillField("ShippingMethod", "Express");
            if (!success)
            {
                Console.Error.WriteLine("Failed to set the ShippingMethod field to Express.");
            }

            form.Save(outputPath);
        }

        Console.WriteLine($"Form saved to '{outputPath}'.");
    }
}
