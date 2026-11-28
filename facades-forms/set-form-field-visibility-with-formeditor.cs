using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output9.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so we instantiate it directly.
        var formEditor = new FormEditor();

        // Bind the PDF document to the FormEditor.
        formEditor.BindPdf(inputPath);

        // Set the appearance of the form field named "EmailAddress" to Visible.
        // Visibility is the default state, represented by no annotation flags (value 0).
        formEditor.SetFieldAppearance("EmailAddress", (AnnotationFlags)0);

        // Save the modified PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}