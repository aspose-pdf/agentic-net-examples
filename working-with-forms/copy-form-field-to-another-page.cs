using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "TextField1";      // existing field name to copy
        const int targetPageNumber = 2;             // page where the copy will be placed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the AcroForm of the document
            Form form = doc.Form;

            // Retrieve the source field (cast from WidgetAnnotation to Field)
            Field sourceField = doc.Form[fieldName] as Field;
            if (sourceField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
                return;
            }

            // Preserve the original rectangle (position and size) of the field
            Aspose.Pdf.Rectangle sourceRect = sourceField.Rect;

            // Create a copy of the field on the target page.
            // The Add method with a partial name returns the newly created field instance.
            string copyPartialName = fieldName + "_Copy";
            Field copiedField = form.Add(sourceField, copyPartialName, targetPageNumber);

            // Add the visual appearance of the copied field on the target page
            form.AddFieldAppearance(copiedField, targetPageNumber, sourceRect);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' successfully copied to page {targetPageNumber} and saved as '{outputPath}'.");
    }
}
