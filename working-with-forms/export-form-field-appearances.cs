using System;
using System.IO;
using Aspose.Pdf;
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

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the source PDF containing form fields
            using (Document sourceDoc = new Document(inputPdf))
            {
                // Access the form object
                Form sourceForm = sourceDoc.Form;

                // Iterate over each field in the form
                foreach (Field field in sourceForm.Fields)
                {
                    // Create a new PDF document to hold the appearance of this field
                    using (Document appearanceDoc = new Document())
                    {
                        // Add a blank page (size will be adjusted later)
                        appearanceDoc.Pages.Add();

                        // Retrieve the rectangle of the field (its size and position)
                        Aspose.Pdf.Rectangle fieldRect = field.Rect;

                        // Add the field's appearance to the first page of the new document
                        // The appearance is placed at the same size as the original field
                        sourceForm.AddFieldAppearance(field, 1, fieldRect);

                        // Optionally, you can remove the field annotation from the new document
                        // to keep only the visual appearance (if desired)
                        // appearanceDoc.Form.Fields.Clear();

                        // Build a safe file name using the field's full name
                        string safeName = MakeSafeFileName(field.FullName);
                        string outputPath = Path.Combine(outputFolder, $"{safeName}.pdf");

                        // Save the appearance document
                        appearanceDoc.Save(outputPath);
                        Console.WriteLine($"Exported appearance of field '{field.FullName}' to '{outputPath}'.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to replace invalid filename characters
    static string MakeSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}