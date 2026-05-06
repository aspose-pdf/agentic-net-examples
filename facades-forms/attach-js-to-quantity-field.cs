using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // JavaScript that updates TotalPrice when Quantity changes
            string jsCode = "var qty = this.getField('Quantity').value;" +
                            "var price = this.getField('UnitPrice').value;" +
                            "this.getField('TotalPrice').value = qty * price;";

            // Attach the script to the Quantity field
            formEditor.SetFieldScript("Quantity", jsCode);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF with attached JavaScript saved to '{outputPath}'.");
    }
}