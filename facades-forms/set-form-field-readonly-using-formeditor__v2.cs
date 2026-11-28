using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // PropertyFlag enum lives here

class Program
{
    static void Main()
    {
        const string inputPath  = "draft_form_v3.pdf";
        const string outputPath = "readonly_status.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor does not implement IDisposable, so we instantiate it directly.
            FormEditor editor = new FormEditor();

            // Load the PDF document into the editor.
            editor.BindPdf(inputPath);

            // Set the "Status" field to read‑only using the correct FormEditor method.
            // The method is SetFieldAttribute(string fieldName, PropertyFlag attribute).
            editor.SetFieldAttribute("Status", PropertyFlag.ReadOnly);

            // Save the modified PDF.
            editor.Save(outputPath);

            Console.WriteLine($"Read‑only form saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
