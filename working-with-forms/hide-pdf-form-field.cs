using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hidden_field.pdf";
        const string fieldName  = "MyField"; // replace with the actual field name

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
                // Access the form object
                Form form = doc.Form;

                // Retrieve the field by its fully qualified name
                // The indexer returns a WidgetAnnotation representing the field
                WidgetAnnotation field = form[fieldName];

                if (field == null)
                {
                    Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                }
                else
                {
                    // Set the Hidden flag on the field's annotation flags
                    field.Flags = field.Flags | AnnotationFlags.Hidden;

                    // Alternatively, you could attach a HideAction to the field's actions:
                    // field.Actions.Add(new HideAction(field, true));
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with hidden field to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}