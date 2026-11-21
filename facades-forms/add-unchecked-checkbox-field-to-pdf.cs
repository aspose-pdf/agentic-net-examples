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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use FormEditor (Facades API) to modify the form structure
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Rectangle coordinates for the checkbox (llx, lly, urx, ury)
            float llx = 100F;
            float lly = 500F;
            float urx = 120F;
            float ury = 520F;

            // Add an unchecked checkbox field named "AgreeTerms" on page 2
            // FormEditor does not have AddCheckBoxField; the correct method is AddField
            editor.AddField(FieldType.CheckBox, "AgreeTerms", 2, llx, lly, urx, ury);

            editor.Save(outputPath);
        }

        Console.WriteLine($"Checkbox field added and saved to '{outputPath}'.");
    }
}
