using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // source PDF with a form
        const string outputPath = "output.pdf";  // destination PDF
        const string sourceFieldName = "TextField1"; // name of the field to duplicate
        const int copies = 5;                    // how many copies to create
        const int pageNumber = 1;                // 1‑based page index where copies will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form; // access the form object

            // Verify that the source field exists
            if (!form.HasField(sourceFieldName))
            {
                Console.Error.WriteLine($"Field '{sourceFieldName}' not found in the document.");
                return;
            }

            // Retrieve the original field (cast to Field because the indexer returns WidgetAnnotation)
            Field originalField = (Field)form[sourceFieldName];

            // Duplicate the field multiple times
            for (int i = 1; i <= copies; i++)
            {
                // New partial name for the duplicated field
                string newPartialName = $"{sourceFieldName}_Copy{i}";

                // Add creates a copy of the original field and places it on the specified page
                Field newField = form.Add(originalField, newPartialName, pageNumber);

                // Optionally offset the rectangle to avoid overlapping fields
                // Shift each copy 20 points to the right
                newField.Rect = new Aspose.Pdf.Rectangle(
                    originalField.Rect.LLX + i * 20,
                    originalField.Rect.LLY,
                    originalField.Rect.URX + i * 20,
                    originalField.Rect.URY);
            }

            // Save the modified PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicated fields saved to '{outputPath}'.");
    }
}