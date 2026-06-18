using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // JavaScript that updates the TotalPrice field when Quantity changes
                string js = "var qty = this.getField('Quantity').value;" +
                            "var unitPrice = 10;" + // example unit price, adjust as needed
                            "this.getField('TotalPrice').value = qty * unitPrice;";

                // Attach the script to the Quantity field
                bool attached = formEditor.SetFieldScript("Quantity", js);
                if (!attached)
                {
                    Console.Error.WriteLine("Failed to attach JavaScript to the 'Quantity' field.");
                }

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with attached JavaScript saved to '{outputPath}'.");
    }
}