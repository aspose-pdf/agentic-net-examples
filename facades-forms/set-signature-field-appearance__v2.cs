using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "application_form_v2.pdf";
        const string outputPath = "signature_appearance.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create FormEditor (does not implement IDisposable)
        FormEditor formEditor = new FormEditor();

        // Bind the source PDF
        formEditor.BindPdf(inputPath);

        // Set the appearance for the "Signature" field.
        // The two‑argument overload applies the given AnnotationFlags to the widget annotation.
        // Use Print flag to make the annotation printable; visibility is the default state.
        formEditor.SetFieldAppearance("Signature", AnnotationFlags.Print);

        // Save the modified PDF
        formEditor.Save(outputPath);

        Console.WriteLine($"Signature appearance set and saved to '{outputPath}'.");
    }
}
