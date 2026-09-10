using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, FieldType

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF to edit
        const string outputPath = "output_with_radio.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Configure the radio button group
        formEditor.RadioGap = 5;                         // gap between buttons (pixels)
        formEditor.RadioHoriz = true;                    // arrange horizontally
        formEditor.Items = new string[] { "Option1", "Option2", "Option3" };

        // Add the radio button field:
        //   FieldType.Radio – type of field
        //   "MyRadioGroup" – logical name of the group
        //   "Option2"      – default selected option (must match one of Items)
        //   1              – page number (1‑based)
        //   100,500,250,520 – rectangle coordinates (llx,lly,urx,ury)
        formEditor.AddField(FieldType.Radio, "MyRadioGroup", "Option2", 1, 100, 500, 250, 520);

        // Persist changes to the output PDF
        formEditor.Save();

        // Release resources
        formEditor.Close();

        Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
    }
}