using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // Form handling API

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a Form facade bound to the loaded document
            Form form = new Form(doc);

            // Retrieve the field facade for the field named "Signature"
            FormFieldFacade signatureField = form.GetFieldFacade("Signature");

            // Apply a custom border thickness of 2 points
            signatureField.BorderWidth = 2;

            // Save the modified document
            form.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}