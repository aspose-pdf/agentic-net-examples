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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Retrieve an existing form field (first one found)
            Field originalField = null;
            foreach (Field f in doc.Form.Fields)
            {
                originalField = f;
                break;
            }

            if (originalField == null)
            {
                Console.WriteLine("No form fields present in the document.");
                doc.Save(outputPath);
                return;
            }

            // Ensure the target page exists (pages are 1‑based)
            int targetPageNumber = 2;
            if (doc.Pages.Count < targetPageNumber)
                doc.Pages.Add(); // add a blank page if needed

            // Clone the field onto the target page with a new partial name.
            // This uses Form.Add(Field, string, int) which creates a copy.
            Field clonedField = doc.Form.Add(originalField, "ClonedField", targetPageNumber);

            // Modify properties of the cloned field
            clonedField.Value = "Cloned value";
            clonedField.ReadOnly = true;
            // Set a new rectangle (position and size) on the target page
            clonedField.Rect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);
            // Change the field's border color for visual distinction
            clonedField.Color = Aspose.Pdf.Color.LightBlue;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cloned field saved to '{outputPath}'.");
    }
}