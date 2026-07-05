using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Facade for editing form fields
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Facade to query field information
                Form formFacade = new Form(doc);

                // Iterate over all field names and remove those that are push‑buttons
                foreach (string fieldName in formFacade.FieldNames)
                {
                    // Determine the field type
                    FieldType fieldType = formFacade.GetFieldType(fieldName);

                    // Remove button (push‑button) fields
                    if (fieldType == FieldType.PushButton)
                    {
                        formEditor.RemoveField(fieldName);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Button fields removed. Output saved to '{outputPath}'.");
    }
}
