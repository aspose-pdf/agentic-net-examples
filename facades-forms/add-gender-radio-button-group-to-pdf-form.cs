using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (must exist) and the resulting PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdf}");
            return;
        }

        // FormEditor loads the source PDF and prepares the output file
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Define the radio button options
            formEditor.Items = new string[] { "Male", "Female", "Other" };

            // Arrange the radios horizontally (default is true, set explicitly for clarity)
            formEditor.RadioHoriz = true;

            // Optional: set the gap between radio buttons (pixels)
            formEditor.RadioGap = 10;

            // Add the radio button group:
            // - FieldType.Radio indicates a radio button group
            // - "Gender" is the field name
            // - "Male" is the default selected option
            // - Page number is 1 (Aspose.Pdf uses 1‑based indexing)
            // - llx, lly, urx, ury define the rectangle that contains the group
            formEditor.AddField(FieldType.Radio, "Gender", "Male", 1, 100, 700, 200, 720);

            // Persist the changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Radio button group 'Gender' added and saved to '{outputPdf}'.");
    }
}