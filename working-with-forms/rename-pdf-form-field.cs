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

        // Example: old field name in the source PDF and the new name required by downstream systems
        const string oldFieldName = "CustomerID";
        const string newFieldName = "Cust_ID";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
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

                    // Rename the field:
                    // - Name: the annotation name on the page
                    // - PartialName: logical name used by the form hierarchy
                    // - MappingName: name used when exporting form data
                    field.Name        = newFieldName;
                    field.PartialName = newFieldName;
                    field.MappingName = newFieldName;
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with renamed field saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}