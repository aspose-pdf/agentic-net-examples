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

        // Load the PDF, add a push button that resets the form, and save the result
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF
            formEditor.BindPdf(inputPath);

            // Add a push button named "ResetForm" on page 1
            // Rectangle coordinates: lower‑left (100,100), upper‑right (200,150)
            formEditor.AddField(FieldType.PushButton, "ResetForm", 1, 100, 100, 200, 150);

            // Attach JavaScript to clear all form fields when the button is clicked
            formEditor.AddFieldScript("ResetForm", "this.resetForm();");

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF with ResetForm button saved to '{outputPath}'.");
    }
}