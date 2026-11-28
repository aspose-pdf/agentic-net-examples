using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for PropertyFlag

class Program
{
    static void Main()
    {
        const string inputPath = "invoice.pdf";
        const string outputPath = "final.pdf";
        const string fieldName = "totalAmount"; // name of the form field to make read‑only

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor is a Facade class; it does not implement IDisposable.
            FormEditor formEditor = new FormEditor();

            // Load the PDF document into the editor.
            formEditor.BindPdf(inputPath);

            // Set the ReadOnly attribute on the specified field.
            // The correct method is SetFieldAttribute, not SetFieldProperty.
            formEditor.SetFieldAttribute(fieldName, PropertyFlag.ReadOnly);

            // Save the modified PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"PDF saved with read‑only field to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
