using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "renamed.pdf";
        const string oldFieldName = "CustomerID";   // existing field name
        const string newFieldName = "Cust_ID";      // new naming convention

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Verify that the form contains fields
            if (form == null || form.Count == 0)
            {
                Console.WriteLine("No form fields present in the document.");
                doc.Save(outputPath);
                return;
            }

            // Ensure the target field exists
            if (!form.HasField(oldFieldName))
            {
                Console.WriteLine($"Field '{oldFieldName}' not found.");
                doc.Save(outputPath);
                return;
            }

            // Retrieve the field by its current name
            Field field = (Field)form[oldFieldName];

            // Rename the field:
            // - Name updates the internal field identifier (PartialName)
            // - MappingName is used when exporting form data to external systems
            field.Name = newFieldName;
            field.MappingName = newFieldName;

            // Optional: update the tooltip (AlternateName) for user visibility
            field.AlternateName = $"Renamed from {oldFieldName}";

            // Save the modified PDF (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field renamed and saved to '{outputPath}'.");
    }
}