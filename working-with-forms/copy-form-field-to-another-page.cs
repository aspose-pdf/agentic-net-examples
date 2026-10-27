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
        const string fieldName = "myField";   // name of the field to copy
        const int sourcePage = 1;               // page where the original field resides (kept for reference)
        const int targetPage = 2;               // page where the copy will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the original field (assumed to be a TextBoxField; adjust type if needed)
            TextBoxField originalField = doc.Form[fieldName] as TextBoxField;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or not a TextBoxField.");
                return;
            }

            // Clone the field to create an independent copy
            TextBoxField copiedField = (TextBoxField)originalField.Clone();

            // Assign a new unique name to avoid name collisions
            copiedField.PartialName = $"Copy_of_{originalField.PartialName}";

            // Preserve the original rectangle (position and size)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                originalField.Rect.LLX,
                originalField.Rect.LLY,
                originalField.Rect.URX,
                originalField.Rect.URY);
            copiedField.Rect = rect;

            // Add the new field to the document's form collection
            doc.Form.Add(copiedField);

            // Place the copied field on the target page using AddFieldAppearance
            doc.Form.AddFieldAppearance(copiedField, targetPage, rect);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' copied to page {targetPage} and saved as '{outputPath}'.");
    }
}
