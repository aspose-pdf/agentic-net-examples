using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a simple
        // three‑page PDF so that the radio button can be placed on page 3.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                // Add three blank pages – the third one will host the field.
                doc.Pages.Add(); // page 1
                doc.Pages.Add(); // page 2
                doc.Pages.Add(); // page 3
                doc.Save(inputPath);
            }
        }

        // ------------------------------------------------------------
        // Open the PDF with FormEditor and add the radio‑button group.
        // ------------------------------------------------------------
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Define the radio button options.
            formEditor.Items = new string[] { "Credit", "PayPal" };

            // Add a radio button group named "PaymentMethod" on page 3.
            // The rectangle (llx, lly, urx, ury) positions the group.
            // "Credit" is set as the initially selected option.
            formEditor.AddField(
                FieldType.Radio,   // field type
                "PaymentMethod", // field name
                "Credit",        // initial selected option
                3,                 // page number (1‑based)
                100, 500,          // lower‑left X and Y
                200, 520);         // upper‑right X and Y

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
    }
}
