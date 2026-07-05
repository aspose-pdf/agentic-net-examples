using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the "Age" field
        const string outputPdf = "output.pdf";  // PDF with attached JavaScript

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the FormEditor facade and bind the source PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // JavaScript to display a warning when the entered value is less than 18
            string jsCode = "if (event.value < 18) { app.alert('You must be at least 18 years old.'); }";

            // Attach the script to the field named "Age"
            // SetFieldScript works for any field; the script will be executed on the field's validation event
            formEditor.SetFieldScript("Age", jsCode);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"JavaScript attached and saved to '{outputPdf}'.");
    }
}