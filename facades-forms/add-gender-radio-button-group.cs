using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (can be empty or existing) and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Initialize FormEditor without destination (new API).
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the source PDF. If the file does not exist, create an empty PDF first.
            if (!System.IO.File.Exists(inputPdf))
            {
                // Create a blank PDF with a single page so that FormEditor can bind to it.
                using (Document blankDoc = new Document())
                {
                    blankDoc.Pages.Add();
                    blankDoc.Save(inputPdf);
                }
            }
            formEditor.BindPdf(inputPdf);

            // Define the radio button options.
            formEditor.Items = new string[] { "Male", "Female", "Other" };

            // Add a radio button group named "Gender" on page 1.
            // The third argument ("Male") sets the default selected option.
            // Coordinates are (llx, lly, urx, ury) in points.
            formEditor.AddField(
                FieldType.Radio,   // field type
                "Gender",          // field name
                "Male",            // default selected value
                1,                 // page number (1‑based)
                100, 700,          // lower‑left corner (x, y)
                200, 720);         // upper‑right corner (x, y)

            // Persist changes to the output PDF using the new Save overload.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Radio button group 'Gender' added to '{outputPdf}'.");
    }
}
