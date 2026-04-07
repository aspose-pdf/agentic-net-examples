using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_tooltip.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (using rule: load with Document)
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the document
            ButtonField submitBtn = new ButtonField(doc, btnRect)
            {
                // Set the field name (used to reference the button)
                PartialName = "SubmitButton",

                // Tooltip shown in Acrobat – use AlternateName property
                AlternateName = "Please fill all required fields before submitting.",

                // Optional: visible caption on the button
                NormalCaption = "Submit"
            };

            // Add the button to the form on page 1 (pages are 1‑based)
            doc.Form.Add(submitBtn, 1);

            // Save the modified PDF (using rule: save with Document)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip: {outputPath}");
    }
}