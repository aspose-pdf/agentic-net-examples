using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for accessing AcroForm fields

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_filled.pdf";
        const string fieldName  = "ShippingMethod"; // name of the radio button group
        const string optionValue = "Express";       // export value of the desired option

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document containing the AcroForm.
        Document pdfDocument = new Document(inputPath);

        // Locate the radio‑button field via the Form.Fields collection.
        Field field = pdfDocument.Form?.Fields?.FirstOrDefault(f =>
            string.Equals(f.PartialName, fieldName, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(f.FullName, fieldName, StringComparison.OrdinalIgnoreCase));

        if (field == null)
        {
            Console.Error.WriteLine($"Form field '{fieldName}' not found in the PDF.");
            return;
        }

        // The field must be a RadioButtonField (or a generic Field that supports Value).
        // Setting the Value property selects the radio button option whose export value matches.
        field.Value = optionValue;

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Radio button '{fieldName}' set to '{optionValue}'. Saved to '{outputPath}'.");
    }
}
