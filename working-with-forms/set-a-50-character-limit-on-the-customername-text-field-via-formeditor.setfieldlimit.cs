using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF document within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor and bind the loaded document
            FormEditor editor = new FormEditor();
            editor.BindPdf(doc);

            // Set a maximum of 50 characters for the field named "CustomerName"
            editor.SetFieldLimit("CustomerName", 50);

            // Save the modified PDF to the output path
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"CustomerName field limit set to 50 characters. Saved to '{outputPath}'.");
    }
}