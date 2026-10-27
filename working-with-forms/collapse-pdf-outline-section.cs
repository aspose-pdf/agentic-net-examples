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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Assume the collapsible section is controlled by a button field named "SectionToggle".
            // Retrieve the field from the document's form collection.
            ButtonField toggleField = doc.Form["SectionToggle"] as ButtonField;

            if (toggleField != null)
            {
                // Set the initial state of the collapsible section to collapsed.
                // In the current Aspose.Pdf version the FieldFlag enum and SetFlag method are not available.
                // The typical way to collapse a section controlled by a button is to set its value to "Off".
                toggleField.Value = "Off";
            }
            else
            {
                Console.WriteLine("Toggle field 'SectionToggle' not found.");
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with collapsible section initially collapsed: {outputPath}");
    }
}
