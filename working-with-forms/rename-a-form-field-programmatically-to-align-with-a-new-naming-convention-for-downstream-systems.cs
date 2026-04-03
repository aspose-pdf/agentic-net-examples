using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF with form fields
        const string outputPath = "renamed_fields.pdf"; // destination PDF
        const string oldFieldName = "CustomerID";       // existing field name
        const string newFieldName = "ClientID";         // new naming convention

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the form contains the field we want to rename
            if (doc.Form.HasField(oldFieldName))
            {
                // Retrieve the field (WidgetAnnotation) by its current name
                Field field = doc.Form[oldFieldName] as Field;

                if (field != null)
                {
                    // Rename the field by setting its Name (full name) and PartialName.
                    // Both properties are writable and affect how the field is identified.
                    field.Name = newFieldName;
                    field.PartialName = newFieldName;

                    // Optionally, also update the MappingName if the downstream system uses it.
                    field.MappingName = newFieldName;

                    Console.WriteLine($"Field '{oldFieldName}' renamed to '{newFieldName}'.");
                }
                else
                {
                    Console.Error.WriteLine($"Field '{oldFieldName}' could not be cast to Field.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Field '{oldFieldName}' does not exist in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Renamed PDF saved to '{outputPath}'.");
    }
}