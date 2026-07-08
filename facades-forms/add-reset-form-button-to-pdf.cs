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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a push button named "ResetForm" on page 1
                // Adjust the rectangle coordinates as needed
                formEditor.AddField(FieldType.PushButton, "ResetForm", 1, 100, 100, 200, 130);

                // Attach JavaScript to clear all form fields when the button is clicked
                formEditor.AddFieldScript("ResetForm", "this.resetForm();");

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with ResetForm button saved to '{outputPath}'.");
    }
}