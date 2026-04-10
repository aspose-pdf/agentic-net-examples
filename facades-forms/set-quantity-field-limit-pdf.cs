using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "FormWithQuantity.pdf";
        const string outputPdf = "FormWithQuantity_Limited.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the FormEditor facade with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Set the maximum character count for the "Quantity" field to 100
                // (this limits the field input length; numeric range enforcement must be handled elsewhere)
                bool result = formEditor.SetFieldLimit("Quantity", 100);
                if (!result)
                {
                    Console.Error.WriteLine("Failed to set field limit for 'Quantity'.");
                }

                // Save the modified PDF to a new file
                formEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Form field limit applied and saved to '{outputPdf}'.");
    }
}