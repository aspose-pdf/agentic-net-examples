using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_reset.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the FormEditor facade on the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Add a push‑button field named "ResetBtn" on page 1.
            // Rectangle coordinates: lower‑left (50, 750), upper‑right (150, 800)
            formEditor.AddField(FieldType.PushButton, "ResetBtn", 1, 50, 750, 150, 800);

            // Attach JavaScript that clears all form fields and restores defaults
            formEditor.SetFieldScript("ResetBtn", "this.resetForm();");

            // Persist the changes to a new PDF file
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPdf}'.");
    }
}