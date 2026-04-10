using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // PDF containing the 'Country' combo box
        const string outputPdf = "output.pdf";     // PDF with the updated default selection

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the combo box field named "Country"
            // The field is part of the core Forms API (Aspose.Pdf.Forms)
            ComboBoxField countryField = doc.Form["Country"] as ComboBoxField;

            if (countryField == null)
            {
                Console.Error.WriteLine("Combo box field 'Country' not found.");
                return;
            }

            // Set the default selected value to "United States".
            // The Value property accepts the export value of the option.
            // If the option list contains a display name different from the export value,
            // use the export value that matches the desired item.
            countryField.Value = "United States";

            // Alternatively, you could set the Selected index if you know the position:
            // countryField.Selected = 3; // example index (1‑based)

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPdf}'.");
    }
}