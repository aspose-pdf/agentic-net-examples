using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "FormWithQuantity.pdf";   // existing PDF containing the "Quantity" field
        const string outputPdf = "FormWithQuantity_Limited.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use FormEditor (a SaveableFacade) inside a using block for deterministic disposal
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document
            formEditor.BindPdf(inputPdf);

            // Set the maximum character count for the "Quantity" field.
            // The limit of 100 characters effectively allows values up to three digits (e.g., 1‑100).
            bool success = formEditor.SetFieldLimit("Quantity", 100);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set field limit for 'Quantity'.");
                return;
            }

            // Save the modified PDF to a new file
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Field limit applied and saved to '{outputPdf}'.");
    }
}