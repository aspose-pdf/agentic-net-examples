using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output16.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a FormEditor facade to work with form fields
        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPath);

        // Configure the appearance on the editor's Facade (not on the field itself)
        editor.Facade.Alignment = FormFieldFacade.AlignCenter; // int constant, not enum
        editor.Facade.BorderColor = System.Drawing.Color.LightGray; // fully qualified to avoid ambiguity
        // Approximate 0.9 (90%) gray background using System.Drawing.Color
        editor.Facade.BackgroundColor = System.Drawing.Color.FromArgb(230, 230, 230);

        // Apply the configured appearance to the specific field "PhoneNumber"
        editor.DecorateField("PhoneNumber");

        // Save the modified PDF
        editor.Save(outputPath);
        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
