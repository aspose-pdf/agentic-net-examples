using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the resulting PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_gender.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use FormEditor (a SaveableFacade) to edit the form.
        // The facade implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPdf);

            // Define the radio button options.
            formEditor.Items = new string[] { "Male", "Female", "Other" };

            // Optional: set visual layout properties.
            formEditor.RadioGap   = 8;   // gap between buttons in pixels
            formEditor.RadioHoriz = true; // arrange horizontally (default is true)

            // Add the radio button field.
            // Parameters: field type, field name, initial value (default selection),
            // page number (1‑based), lower‑left x, lower‑left y, upper‑right x, upper‑right y.
            // Adjust the rectangle coordinates as needed for your PDF layout.
            bool added = formEditor.AddField(
                FieldType.Radio,
                "Gender",
                "Male",          // default selected option
                1,               // page number
                100, 500,        // llx, lly
                300, 520);       // urx, ury

            if (!added)
            {
                Console.Error.WriteLine("Failed to add the Gender radio button field.");
                return;
            }

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Gender radio button group added. Output saved to '{outputPdf}'.");
    }
}