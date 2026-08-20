using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "TermsAccepted";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF form
        using (Form form = new Form(inputPath))
        {
            // Set the checkbox field to true
            bool success = form.FillField(fieldName, true);
            if (!success)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or could not be filled.");
            }

            // Save the modified PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"Checkbox '{fieldName}' set to true and saved to '{outputPath}'.");
    }
}