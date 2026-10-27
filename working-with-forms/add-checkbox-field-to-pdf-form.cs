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
            // Load the existing PDF
            using (Document doc = new Document(inputPath))
            {
                // Define the rectangle for the checkbox (lower‑left X,Y and upper‑right X,Y)
                var rect = new Rectangle(100, 700, 120, 720);

                // Create a new checkbox field on the first page
                var checkBox = new CheckboxField(doc.Pages[1], rect)
                {
                    // Optional: set the initial state
                    Checked = true
                };
                // Set the field name (PartialName) separately – the constructor does not accept a name argument in recent versions
                checkBox.PartialName = "AgreeTerms";

                // Add the field to the document's form collection
                doc.Form.Add(checkBox);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Checkbox field added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
