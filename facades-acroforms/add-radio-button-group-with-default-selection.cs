using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (must exist) and the output PDF.
        const string sourcePdf = "input.pdf";
        const string outputPdf = "output_with_radio.pdf";

        // Ensure the source file exists.
        if (!System.IO.File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Use FormEditor (a facade) to add a radio button group.
        // The constructor takes the input PDF and the desired output PDF.
        using (FormEditor formEditor = new FormEditor(sourcePdf, outputPdf))
        {
            // Arrange radios horizontally (default is true, set explicitly for clarity).
            formEditor.RadioHoriz = true;

            // Gap between neighboring radio buttons (in pixels).
            formEditor.RadioGap = 5;

            // Define the three options for the radio group.
            formEditor.Items = new string[] { "Option1", "Option2", "Option3" };

            // Add the radio button field.
            // Parameters:
            //   FieldType.Radio          – type of field.
            //   "MyRadioGroup"           – name of the field.
            //   "Option2"                – default selected option.
            //   1                        – page number (1‑based).
            //   100, 500, 200, 520      – lower‑left (llx,lly) and upper‑right (urx,ury) coordinates.
            formEditor.AddField(FieldType.Radio, "MyRadioGroup", "Option2", 1, 100, 500, 200, 520);

            // Persist changes to the output PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Radio button group added and saved to '{outputPdf}'.");
    }
}