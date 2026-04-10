using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the FormEditor facade with the loaded document
            using (FormEditor editor = new FormEditor(doc))
            {
                // Add a hidden text field named "Version" with the value "2"
                // The field is placed on page 1; a zero‑size rectangle is sufficient because the field will be hidden
                editor.AddField(FieldType.Text, "Version", "2", 1, 0, 0, 0, 0);

                // Hide the field by setting the Hidden annotation flag
                editor.SetFieldAppearance("Version", AnnotationFlags.Hidden);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden numeric field 'Version' added and saved to '{outputPath}'.");
    }
}