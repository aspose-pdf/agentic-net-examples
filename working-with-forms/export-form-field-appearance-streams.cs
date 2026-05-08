using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "FieldAppearances";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the source PDF (lifecycle rule: use Document constructor inside a using block)
        using (Document sourceDoc = new Document(inputPdf))
        {
            int fieldCounter = 1;

            // Iterate over all form fields (the Form implements ICollection<WidgetAnnotation>)
            foreach (WidgetAnnotation field in sourceDoc.Form)
            {
                // Clone the field so it can be added to a new document without affecting the source
                Field clonedField = (Field)field.Clone();

                // Create a new PDF that will contain only this field's appearance
                using (Document appearanceDoc = new Document())
                {
                    // Add a blank page to host the appearance
                    appearanceDoc.Pages.Add();

                    // Add the cloned field to the new document's form collection
                    appearanceDoc.Form.Add(clonedField);

                    // Define a rectangle where the appearance will be placed
                    // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                    Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 500, 200);

                    // Add the field's appearance to page 1 of the new document
                    // (Form.AddFieldAppearance is the correct API for this operation)
                    appearanceDoc.Form.AddFieldAppearance(clonedField, 1, rect);

                    // Build a safe file name based on the field's full name (fallback to index)
                    string safeName = MakeSafeFileName(clonedField.FullName ?? $"Field_{fieldCounter}");
                    string outputPath = Path.Combine(outputFolder, $"{safeName}.pdf");

                    // Save the document (lifecycle rule: use Document.Save inside the using block)
                    appearanceDoc.Save(outputPath);
                }

                fieldCounter++;
            }
        }

        Console.WriteLine("Export of form field appearance streams completed.");
    }

    // Helper to replace invalid filename characters
    static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');
        return name;
    }
}