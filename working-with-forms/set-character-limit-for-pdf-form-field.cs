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

        // FormEditor is a Facades class that allows editing form fields.
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPath);

            // Set a maximum of 50 characters for the field named "CustomerName".
            editor.SetFieldLimit("CustomerName", 50);

            // Save the updated PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"CustomerName field limited to 50 characters and saved to '{outputPath}'.");
    }
}