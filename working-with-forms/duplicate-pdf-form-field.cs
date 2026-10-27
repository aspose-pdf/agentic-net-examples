using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_duplicated_fields.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Page that contains the original field – required for the field constructor
            Page page = doc.Pages[1];

            // Retrieve the existing field; the Form indexer returns a WidgetAnnotation
            TextBoxField? existingField = doc.Form["Field1"] as TextBoxField;
            if (existingField == null)
            {
                Console.Error.WriteLine("Field 'Field1' not found or is not a TextBoxField.");
                return;
            }

            const int duplicateCount = 5;
            for (int i = 1; i <= duplicateCount; i++)
            {
                string newFieldName = $"Field1_Copy{i}";
                Aspose.Pdf.Rectangle rect = existingField.Rect;

                // Correct constructor signature: (Page, Rectangle)
                TextBoxField newField = new TextBoxField(page, rect)
                {
                    PartialName = newFieldName,
                    Value = existingField.Value,
                    // Use the property that exists in the target version. If IsReadOnly is unavailable, ReadOnly is the alternative.
                    ReadOnly = existingField.ReadOnly,
                    Border = existingField.Border
                    // BackgroundColor is not available in this version of Aspose.Pdf, so it is omitted.
                };

                // Add the duplicated field to the document's form collection
                doc.Form.Add(newField);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated fields saved to '{outputPath}'.");
    }
}
