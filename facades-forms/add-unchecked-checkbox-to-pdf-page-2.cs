using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for FieldType enum

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

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize the FormEditor facade for the document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Define the checkbox rectangle (lower‑left x,y and upper‑right x,y)
                // Adjust these values as needed for the desired position/size
                float llx = 100f; // lower‑left x
                float lly = 500f; // lower‑left y
                float urx = 120f; // upper‑right x
                float ury = 520f; // upper‑right y

                // Add an unchecked checkbox named "AgreeTerms" on page 2
                formEditor.AddField(FieldType.CheckBox, "AgreeTerms", 2, llx, lly, urx, ury);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Checkbox field added and saved to '{outputPath}'.");
    }
}