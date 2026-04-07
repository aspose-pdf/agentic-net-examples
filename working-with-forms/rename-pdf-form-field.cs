using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Example old and new field names according to the naming convention
        const string oldFieldName = "CustomerID";
        const string newFieldName = "Cust_ID";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Verify that the field to be renamed exists
            if (!form.HasField(oldFieldName))
            {
                Console.WriteLine($"Field '{oldFieldName}' not found in the document.");
            }
            else
            {
                // Retrieve the field (indexer returns WidgetAnnotation, cast to Field)
                Field field = (Field)form[oldFieldName];

                // Rename the field by updating its Name and PartialName properties
                field.Name = newFieldName;
                field.PartialName = newFieldName;

                // Optionally update MappingName for downstream export scenarios
                field.MappingName = newFieldName;

                Console.WriteLine($"Field renamed from '{oldFieldName}' to '{newFieldName}'.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Renamed PDF saved to '{outputPath}'.");
    }
}