using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output7.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use FormEditor to edit form fields
        using (FormEditor editor = new FormEditor())
        {
            // Bind the PDF document
            editor.BindPdf(inputPath);

            // Set maximum length (limit) of the field "PhoneNumber" to 10 characters
            editor.SetFieldLimit("PhoneNumber", 10);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
