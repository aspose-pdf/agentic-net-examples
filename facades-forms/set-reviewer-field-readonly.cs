using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for PropertyFlag

class Program
{
    static void Main()
    {
        const string inputPath = "feedback_form_v3.pdf";
        const string outputPath = "readonly_reviewer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so we instantiate it directly.
        FormEditor formEditor = new FormEditor();

        // Load the PDF into the editor.
        formEditor.BindPdf(inputPath);

        // Set the ReadOnly attribute on the field named "Reviewer".
        // Use SetFieldAttribute with the PropertyFlag enum (Aspose.Pdf.Annotations).
        formEditor.SetFieldAttribute("Reviewer", PropertyFlag.ReadOnly);

        // Save the modified PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"PDF saved with read‑only Reviewer field: '{outputPath}'.");
    }
}
