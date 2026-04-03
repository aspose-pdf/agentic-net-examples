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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the position and size of the new form field (left, bottom, right, top)
            Rectangle fieldRect = new Rectangle(100, 500, 300, 550);

            // Create a text box form field on the first page
            TextBoxField textBox = new TextBoxField(doc.Pages[1], fieldRect)
            {
                PartialName = "MyTextBox",          // internal name of the field
                Value = "Enter text here"           // default value
            };

            // Insert the new field at a specific index within the Form collection.
            // Index is zero‑based; 0 inserts at the beginning, 1 after the first field, etc.
            int insertIndex = 0; // change as needed for desired order
            // Aspose.Pdf.Form does not expose Insert; use Add overload with index.
            doc.Form.Add(textBox, insertIndex);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field inserted and PDF saved to '{outputPath}'.");
    }
}
