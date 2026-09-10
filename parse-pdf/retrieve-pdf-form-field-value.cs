using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string fieldName = "MyField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the AcroForm collection
            Form acroForm = doc.Form;

            try
            {
                // Retrieve the form field by name using the indexer
                WidgetAnnotation widget = acroForm[fieldName];

                // Most form fields are of type Field, which provides a Value property
                if (widget is Field formField)
                {
                    Console.WriteLine($"Field '{fieldName}' value: {formField.Value}");
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' retrieved, but it is not a standard form field.");
                }
            }
            catch (Exception ex)
            {
                // The indexer throws if the field is not found
                Console.WriteLine($"Error retrieving field '{fieldName}': {ex.Message}");
            }
        }
    }
}