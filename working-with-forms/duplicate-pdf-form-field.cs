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
        const string originalFieldName = "TextField1";
        const int copies = 5;
        const double verticalOffset = 20; // shift each copy down by 20 units

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF with a form
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field to be duplicated
            Field originalField = doc.Form[originalFieldName] as Field;
            if (originalField == null)
            {
                Console.Error.WriteLine($"Field '{originalFieldName}' not found.");
                return;
            }

            // Store original rectangle and page number (1‑based)
            Aspose.Pdf.Rectangle origRect = originalField.Rect;
            int pageNumber = originalField.PageIndex;

            // Create copies
            for (int i = 1; i <= copies; i++)
            {
                string newName = $"{originalFieldName}_Copy{i}";

                // Add a copy of the field; this creates a new field instance
                Field copy = doc.Form.Add(originalField, newName, pageNumber);

                // Adjust position of the copy (optional)
                Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(
                    origRect.LLX,
                    origRect.LLY - i * verticalOffset,
                    origRect.URX,
                    origRect.URY - i * verticalOffset);

                copy.Rect = newRect;
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated fields saved to '{outputPath}'.");
    }
}