using System;
using System.IO;
using Aspose.Pdf.Facades; // FormEditor and PropertyFlag are defined here

class Program
{
    static void Main()
    {
        const string inputPath  = "questionnaire_form.pdf";
        const string outputPath = "readonly_date.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a FormEditor instance (does NOT implement IDisposable)
        FormEditor formEditor = new FormEditor();

        // Bind the existing PDF document to the editor
        formEditor.BindPdf(inputPath);

        // Set the "ReadOnly" attribute on the field named "Date"
        // Use SetFieldAttribute with the PropertyFlag.ReadOnly flag.
        formEditor.SetFieldAttribute("Date", PropertyFlag.ReadOnly);

        // Save the modified PDF to a new file
        formEditor.Save(outputPath);

        Console.WriteLine($"PDF saved with read‑only Date field: {outputPath}");
    }
}
