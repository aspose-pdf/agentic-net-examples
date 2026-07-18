using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF using the Form facade (provides field access)
        using (Form form = new Form(inputPath))
        {
            // Retrieve the facade for the field named "Signature"
            FormFieldFacade signatureField = form.GetFieldFacade("Signature");
            if (signatureField == null)
            {
                Console.Error.WriteLine("Field 'Signature' not found.");
                return;
            }

            // Apply a custom border thickness of 2 points
            signatureField.BorderWidth = 2;

            // Save the modified document
            form.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}