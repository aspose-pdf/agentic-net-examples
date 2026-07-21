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

        // FormEditor works with AcroForm fields. It provides methods to modify field
        // attributes such as converting a single‑line text field to a multi‑line one.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the source PDF
            formEditor.BindPdf(inputPdf);

            // Convert the "Address" field from a single‑line to a multi‑line field.
            // This enables word‑wrap and allows the field height to expand as needed.
            bool converted = formEditor.Single2Multiple("Address");
            if (!converted)
            {
                Console.Error.WriteLine("Failed to convert the 'Address' field to multiline.");
                return;
            }

            // Optionally, you can set a maximum character limit or other visual attributes here.
            // For example, to allow up to 500 characters:
            // formEditor.SetFieldLimit("Address", 500);

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field 'Address' updated and saved to '{outputPdf}'.");
    }
}