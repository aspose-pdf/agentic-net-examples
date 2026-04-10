using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the form field
        const string outputPdf = "output.pdf";  // PDF after setting the field limit

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block for deterministic disposal
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF
            formEditor.BindPdf(inputPdf);

            // Set the maximum character length of the "PhoneNumber" field to 15
            bool result = formEditor.SetFieldLimit("PhoneNumber", 15);
            if (!result)
            {
                Console.Error.WriteLine("Failed to set field limit for 'PhoneNumber'.");
                return;
            }

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field limit applied. Output saved to '{outputPdf}'.");
    }
}