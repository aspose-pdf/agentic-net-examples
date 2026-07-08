using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "renamed.pdf";
        const string oldFieldName = "oldField";
        const string newFieldName = "newField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Verify that the field exists
                if (!doc.Form.HasField(oldFieldName))
                {
                    Console.WriteLine($"Field '{oldFieldName}' not found.");
                }
                else
                {
                    // Retrieve the field (WidgetAnnotation) and cast to Field
                    var field = doc.Form[oldFieldName] as Field;
                    if (field != null)
                    {
                        // Rename the field
                        field.Name = newFieldName;            // Annotation name
                        field.PartialName = newFieldName;     // Partial name (optional)
                        field.MappingName = newFieldName;     // Mapping name for export (optional)
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Renamed field saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}