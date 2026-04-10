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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a FormEditor bound to the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Set the field "TotalAmount" to read‑only
            formEditor.SetFieldAttribute("TotalAmount", PropertyFlag.ReadOnly);

            // Apply a visual appearance flag (example: make the field printable)
            formEditor.SetFieldAppearance("TotalAmount", AnnotationFlags.Print);

            // Fill the field with a currency‑formatted value
            Form form = new Form(doc);
            form.FillField("TotalAmount", "$1234.56");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}