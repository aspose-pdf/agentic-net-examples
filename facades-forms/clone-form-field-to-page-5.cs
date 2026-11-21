using System;
using System.IO;
using Aspose.Pdf.Facades; // FormEditor resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor does NOT implement IDisposable – do NOT wrap in using
        FormEditor formEditor = new FormEditor();

        // Load the PDF document into the facade
        formEditor.BindPdf(inputPath);

        // Copy the outer (appearance) definition of "TemplateField"
        // to a new field named "ClonedField" placed on page 5
        formEditor.CopyOuterField("TemplateField", "ClonedField", 5);

        // Persist the changes to a new file
        formEditor.Save(outputPath);

        Console.WriteLine($"Cloned field saved to '{outputPath}'.");
    }
}