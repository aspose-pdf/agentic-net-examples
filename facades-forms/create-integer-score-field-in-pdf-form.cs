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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the field on
            Page page = doc.Pages[1];

            // Define the field rectangle (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 530);

            // Create a NumberField (inherits TextBoxField) named "Score"
            NumberField scoreField = new NumberField(page, rect);
            scoreField.PartialName = "Score";

            // Allow only numeric characters
            scoreField.AllowedChars = "0123456789";

            // Limit the field to three characters (covers 0‑100)
            scoreField.MaxLen = 3;

            // Optional: set an initial value
            scoreField.Value = "0";

            // Add the field to the document's form
            doc.Form.Add(scoreField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with 'Score' field constraints to '{outputPath}'.");
    }
}