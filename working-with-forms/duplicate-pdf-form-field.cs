using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string sourceFieldName = "Field1";   // name of the field to duplicate
        const int copies = 5;                      // number of duplicate fields to create

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the source field; ensure it exists and is a TextBoxField
            TextBoxField sourceField = doc.Form[sourceFieldName] as TextBoxField;
            if (sourceField == null)
            {
                Console.Error.WriteLine($"Form field '{sourceFieldName}' not found or not a TextBoxField.");
                return;
            }

            // Base rectangle of the source field (used to position duplicates)
            Aspose.Pdf.Rectangle baseRect = sourceField.Rect;

            // Create the requested number of copies
            for (int i = 1; i <= copies; i++)
            {
                // Clone the original field
                TextBoxField clone = (TextBoxField)sourceField.Clone();

                // Assign a unique name to the cloned field
                clone.PartialName = $"{sourceFieldName}_{i}";

                // Optional: shift each clone down to avoid overlap
                double margin = 5; // space between fields
                double height = baseRect.URY - baseRect.LLY;
                double shift  = (height + margin) * i;

                clone.Rect = new Aspose.Pdf.Rectangle(
                    baseRect.LLX,
                    baseRect.LLY - shift,
                    baseRect.URX,
                    baseRect.URY - shift);

                // Add the cloned field to the document's form collection
                doc.Form.Add(clone);
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated fields saved to '{outputPath}'.");
    }
}