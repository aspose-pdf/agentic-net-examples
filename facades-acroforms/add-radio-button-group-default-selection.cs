using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify the source PDF exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Initialize FormEditor with source and destination files
        FormEditor formEditor = new FormEditor(inputPdf, outputPdf);

        // Configure radio button layout
        formEditor.RadioGap  = 5;      // Gap between buttons (pixels)
        formEditor.RadioHoriz = true; // Arrange horizontally (true) or vertically (false)

        // Define the three radio options
        formEditor.Items = new string[] { "Option1", "Option2", "Option3" };

        // Add the radio button group:
        // FieldType.Radio – type of field
        // "MyRadioGroup" – field name
        // "Option2" – default selected option (must match one of the Items)
        // 1 – page number (1‑based)
        // 100, 500, 300, 520 – rectangle coordinates (llx, lly, urx, ury)
        formEditor.AddField(FieldType.Radio, "MyRadioGroup", "Option2", 1, 100, 500, 300, 520);

        // Persist changes to the output PDF
        formEditor.Save();
    }
}