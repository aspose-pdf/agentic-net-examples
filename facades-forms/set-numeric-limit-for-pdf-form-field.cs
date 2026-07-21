using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF containing the "Quantity" field
        const string outputPdf = "output.pdf";  // PDF with the updated field limit

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor is a facade for editing AcroForm fields.
        // It implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF.
            formEditor.BindPdf(inputPdf);

            // Set the maximum character count for the "Quantity" field.
            // A limit of 3 characters allows values from 1 up to 100.
            bool success = formEditor.SetFieldLimit("Quantity", 3);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set field limit for 'Quantity'.");
                return;
            }

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field limit applied and saved to '{outputPdf}'.");
    }
}