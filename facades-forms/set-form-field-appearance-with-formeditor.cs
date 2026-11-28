using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "application_form_v3.pdf";
        const string outputPath = "datefield_appearance.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a FormEditor facade and bind it to the document
            FormEditor formEditor = new FormEditor();
            formEditor.BindPdf(doc);

            // Set the appearance of the "DateField" using AnnotationFlags
            // The field is visible by default; we add the Print flag to make it printable.
            formEditor.SetFieldAppearance("DateField", AnnotationFlags.Print);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}
