using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor on the loaded document
            using (FormEditor editor = new FormEditor(doc))
            {
                // Make the field "TotalAmount" read‑only
                editor.SetFieldAttribute("TotalAmount", PropertyFlag.ReadOnly);

                // Apply appearance flags (e.g., ensure the field is printable)
                editor.SetFieldAppearance("TotalAmount", AnnotationFlags.Print);

                // Optionally set a currency formatted placeholder value
                Form form = new Form(doc);
                form.FillField("TotalAmount", "$0.00");

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}