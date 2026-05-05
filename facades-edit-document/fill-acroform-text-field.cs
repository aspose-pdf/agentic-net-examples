using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the Form facade with input and output files
        Form form = new Form(inputPath, outputPath);

        // Fill the AcroForm text field named "CustomerName"
        bool success = form.FillField("CustomerName", "Acme Corporation");
        if (!success)
        {
            Console.Error.WriteLine("Failed to locate or fill the field 'CustomerName'.");
        }

        // Persist the changes to the output PDF
        form.Save();

        Console.WriteLine($"AcroForm field filled and saved to '{outputPath}'.");
    }
}