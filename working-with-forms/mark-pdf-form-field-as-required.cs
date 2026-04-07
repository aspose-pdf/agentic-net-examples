using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_required.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            if (form.HasField(fieldName))
            {
                // Retrieve existing field and mark it as required
                // The Form indexer returns a WidgetAnnotation, so cast it to Field
                Field? field = form[fieldName] as Field;
                if (field != null)
                {
                    field.Required = true;
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' exists but is not a form field.");
                }
            }
            else
            {
                // Create a new text box field on the first page if it does not exist
                Page page = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);
                TextBoxField txtField = new TextBoxField(page, rect)
                {
                    PartialName = fieldName,
                    Required = true
                };
                form.Add(txtField);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with required field: {outputPath}");
    }
}
