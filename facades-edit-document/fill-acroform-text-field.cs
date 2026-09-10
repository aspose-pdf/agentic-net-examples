using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF that contains the AcroForm
        using (Form form = new Form(inputPath))
        {
            // Fill the text field named "CustomerName" with the desired value
            bool success = form.FillField("CustomerName", "Acme Corporation");
            if (!success)
            {
                Console.Error.WriteLine("Failed to fill the field 'CustomerName'.");
            }

            // Save the updated PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"AcroForm field filled and saved to '{outputPath}'.");
    }
}