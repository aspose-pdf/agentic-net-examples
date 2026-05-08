using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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
            // Create a FormEditor facade for the document
            FormEditor formEditor = new FormEditor(doc);

            // Example of using SetFieldAttribute (required by the task)
            // Here we set the field as read‑only; you can choose any PropertyFlag you need.
            formEditor.SetFieldAttribute("Signature", PropertyFlag.ReadOnly);

            // Retrieve the signature field from the document's form collection
            Field? signatureField = doc.Form["Signature"] as Field;

            if (signatureField != null)
            {
                // Set a custom border thickness of 2 points
                // Border constructor requires the parent annotation (the field itself)
                signatureField.Border = new Border(signatureField) { Width = 2 };
            }
            else
            {
                Console.Error.WriteLine("Signature field not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}