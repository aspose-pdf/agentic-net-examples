using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "renamed_fields.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Example: rename a field called "OldFieldName" to "NewFieldName"
            const string oldName = "OldFieldName";
            const string newName = "NewFieldName";

            // Verify the field exists
            if (form.HasField(oldName))
            {
                // The Form indexer returns a WidgetAnnotation; cast it to Field
                Field? field = form[oldName] as Field;
                if (field != null)
                {
                    // Rename the field by setting its Name property (partial name)
                    field.Name = newName;
                    // Optionally, also update the MappingName if the downstream system
                    // relies on the export mapping name.
                    field.MappingName = newName;
                    Console.WriteLine($"Field '{oldName}' renamed to '{newName}'.");
                }
                else
                {
                    Console.WriteLine($"Field '{oldName}' exists but could not be cast to a form Field.");
                }
            }
            else
            {
                Console.WriteLine($"Field '{oldName}' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Renamed PDF saved to '{outputPath}'.");
    }
}
