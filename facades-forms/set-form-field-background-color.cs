using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // Required for AnnotationFlags

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // FormEditor works with form fields in the document
            FormEditor formEditor = new FormEditor(doc);

            // Set the appearance of the field named "Status".
            // The available overload takes the field name and an AnnotationFlags value.
            // Here we use AnnotationFlags.Print to ensure the field is printed.
            formEditor.SetFieldAppearance("Status", AnnotationFlags.Print);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
