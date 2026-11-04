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

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify that the document contains at least one form field
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields present in the document.");
                    return;
                }

                // Retrieve the first form field (assumed to be a TextBoxField for this example)
                TextBoxField originalField = doc.Form[0] as TextBoxField;
                if (originalField == null)
                {
                    Console.WriteLine("The first form field is not a TextBoxField.");
                    return;
                }

                // Clone the original field (deep copy of its settings)
                TextBoxField clonedTemplate = (TextBoxField)originalField.Clone();

                // Determine the target page (page numbers are 1‑based)
                int targetPageNumber = 2;
                if (doc.Pages.Count < targetPageNumber)
                {
                    // Add a blank page if the target page does not exist
                    doc.Pages.Add();
                }

                // Define the rectangle where the cloned field will appear
                // (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
                Rectangle rect = new Rectangle(100, 500, 300, 550);

                // Create a new TextBoxField on the target page using the rectangle.
                // This effectively places the cloned field on a different page.
                TextBoxField clonedField = new TextBoxField(doc.Pages[targetPageNumber], rect)
                {
                    // Copy desired properties from the cloned template
                    PartialName = "ClonedField",          // New field name
                    Value = "Cloned value",               // Default value
                    // You can copy additional settings here, e.g., Font, FontSize, etc.
                };

                // Add the cloned field to the document's form collection
                doc.Form.Add(clonedField);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Cloned form field added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
