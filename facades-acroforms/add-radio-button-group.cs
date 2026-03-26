using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "radio_form.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            FormEditor formEditor = new FormEditor(inputPath, outputPath);
            formEditor.RadioGap = 4f;
            formEditor.RadioHoriz = false;
            formEditor.Items = new string[] { "First", "Second", "Third" };
            // Add a radio button group on page 1 with the default selected option "Second"
            formEditor.AddField(FieldType.Radio, "AddedRadioButtonField", "Second", 1, 10f, 30f, 110f, 130f);
            formEditor.Save();
            Console.WriteLine($"Radio button group added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
