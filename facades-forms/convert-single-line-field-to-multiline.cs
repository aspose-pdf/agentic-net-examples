using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the "Address" field
        const string outputPdf = "output.pdf";  // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF with FormEditor (facade for form manipulation)
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPdf);

            // Convert the single‑line "Address" field to a multi‑line field.
            // This enables automatic word‑wrap inside the field.
            bool converted = formEditor.Single2Multiple("Address");
            if (!converted)
            {
                Console.Error.WriteLine("Failed to convert the field to multiline.");
                return;
            }

            // Optionally, you can adjust the field height if needed.
            // The height will expand automatically when the field is multiline
            // and the content exceeds the original rectangle.

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field \"Address\" set to auto‑wrap and saved to '{outputPdf}'.");
    }
}