using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output25.pdf";
        const string fieldName = "AddressLine";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create FormEditor and bind the PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Set the alignment of the specified form field to Right
            // SetFieldAlignment expects an int, so cast the HorizontalAlignment enum value.
            formEditor.SetFieldAlignment(fieldName, (int)HorizontalAlignment.Right);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
