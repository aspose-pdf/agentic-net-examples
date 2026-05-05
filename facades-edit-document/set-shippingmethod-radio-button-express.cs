using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use the Form facade directly and ensure it is disposed properly.
        // This avoids keeping the source PDF file locked which can cause the
        // "GenerateDepsFile" MSBuild error when the project is built in parallel.
        using (Form form = new Form())
        {
            // Bind the existing PDF file.
            form.BindPdf(inputPath);

            // Fill the radio‑button field "ShippingMethod" with the option "Express".
            bool filled = form.FillField("ShippingMethod", "Express");
            if (!filled)
            {
                Console.Error.WriteLine("Failed to fill the field 'ShippingMethod'.");
            }

            // Save the updated PDF to a new file.
            form.Save(outputPath);
        }

        Console.WriteLine($"Radio button set to 'Express' and saved to '{outputPath}'.");
    }
}
