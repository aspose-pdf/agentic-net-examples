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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set field appearance flags using FormEditor
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Example: make the field printable (you can combine flags as needed)
                formEditor.SetFieldAppearance("Price", AnnotationFlags.Print);
            }

            // Fill the "Price" field with a value formatted to two decimal places
            Form form = new Form(doc);
            form.FillField("Price", "123.45"); // ensure the value has two decimal places

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field \"Price\" updated and saved to '{outputPath}'.");
    }
}