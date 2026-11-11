using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_submit.pdf";
        const string buttonName = "SubmitForm";
        const string submitUrl = "https://api.example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialise FormEditor and bind the document
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(doc);

                // Define button rectangle (llx, lly, urx, ury)
                float llx = 100f;
                float lly = 700f;
                float urx = 200f;
                float ury = 750f;

                // Add a push‑button field on page 1 (use FieldType.PushButton)
                editor.AddField(FieldType.PushButton, buttonName, 1, llx, lly, urx, ury);

                // Set the URL that the button will submit to
                editor.SetSubmitUrl(buttonName, submitUrl);

                // Save the modified PDF via the editor
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
