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
        const string fieldName = "Address";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor is the correct facade for editing form fields.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document.
            formEditor.BindPdf(inputPath);

            // Set the alignment of the "Address" field to center.
            formEditor.SetFieldAlignment(fieldName, (int)HorizontalAlignment.Center);

            // Save the updated PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field \"{fieldName}\" alignment set to center. Saved to '{outputPath}'.");
    }
}
