using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (must contain a form) and the output PDF
        const string inputPdf  = "input_form.pdf";
        const string outputPdf = "output_with_radio.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Create a FormEditor facade, binding the input PDF and specifying the output file
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Configure radio button appearance
        formEditor.RadioHoriz = true;               // Arrange radios horizontally
        formEditor.RadioGap   = 5;                  // Gap between radios in pixels
        formEditor.Items      = new string[] { "Option1", "Option2", "Option3" };

        // Add the radio button group:
        // FieldType.Radio – type of field
        // "MyRadioGroup" – name of the field
        // "Option2" – default selected option (must match one of the Items)
        // 1 – page number (1‑based)
        // llx, lly, urx, ury – rectangle coordinates for the first radio button
        formEditor.AddField(FieldType.Radio, "MyRadioGroup", "Option2", 1, 50, 700, 150, 720);

        // Persist changes to the output PDF
        formEditor.Save();

        // Optional: release resources
        formEditor.Close();

        Console.WriteLine($"Radio button group added and saved to '{outputPdf}'.");
    }
}