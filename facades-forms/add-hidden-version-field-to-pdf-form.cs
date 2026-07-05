using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for AnnotationFlags

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
            // Initialize FormEditor facade on the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a hidden numeric field named "Version" on page 1.
                // Coordinates are set to a zero‑size rectangle (off‑page) because the field is hidden.
                bool added = formEditor.AddField(FieldType.Text, "Version", 1, 0, 0, 0, 0);
                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the 'Version' field.");
                    return;
                }

                // Retrieve the newly created field as a TextBoxField (the concrete type for a text field).
                TextBoxField versionField = doc.Form["Version"] as TextBoxField;
                if (versionField == null)
                {
                    Console.Error.WriteLine("Unable to locate the 'Version' field after creation.");
                    return;
                }

                // Mark the field as hidden so it does not appear in the UI or printing.
                versionField.Flags = AnnotationFlags.Hidden;

                // Set the field value (numeric 2). The field type is Text, so store as string.
                versionField.Value = "2";

                // Save the modified PDF.
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with hidden 'Version' field saved to '{outputPath}'.");
    }
}
