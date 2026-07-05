using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Edit the form using the FormEditor facade
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document
            formEditor.BindPdf(inputPath);

            // Set the maximum character length for the "PhoneNumber" field to 15
            bool success = formEditor.SetFieldLimit("PhoneNumber", 15);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set field limit for 'PhoneNumber'.");
            }

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PhoneNumber field limit set to 15. Saved as '{outputPath}'.");
    }
}