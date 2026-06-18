using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "FieldAppearances";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the source PDF
        using (Document sourceDoc = new Document(inputPdfPath))
        {
            int fieldCounter = 0;

            // Iterate over all form fields in the document
            foreach (Field field in sourceDoc.Form.Fields)
            {
                fieldCounter++;

                // Create a new PDF document that will contain only this field's appearance
                using (Document fieldDoc = new Document())
                {
                    // Add a blank page to the new document
                    Page page = fieldDoc.Pages.Add();

                    // Clone the field into the new document
                    // (Add creates a copy of the field that belongs to the new document)
                    fieldDoc.Form.Add(field);

                    // Determine a rectangle for the appearance.
                    // Use the original field rectangle; if it is null, use a default size.
                    Aspose.Pdf.Rectangle appearanceRect = field.Rect ?? new Aspose.Pdf.Rectangle(0, 0, 200, 50);

                    // Add the field's appearance to the first page of the new document
                    // This copies the visual representation (appearance stream) of the field.
                    fieldDoc.Form.AddFieldAppearance(field, 1, appearanceRect);

                    // Build a safe file name based on the field's partial name (or a fallback)
                    string safeName = MakeSafeFileName(field.PartialName ?? $"field_{fieldCounter}");
                    string outputPath = Path.Combine(outputFolder, $"{safeName}.pdf");

                    // Save the document containing only this field's appearance
                    fieldDoc.Save(outputPath);
                }
            }
        }

        Console.WriteLine("Form field appearance export completed.");
    }

    // Helper to replace invalid filename characters
    private static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}