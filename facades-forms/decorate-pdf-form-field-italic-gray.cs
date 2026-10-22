using System;
using System.IO;
using System.Drawing;                     // for Color (fully qualified when used)
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // FormEditor, FontStyle, FormFieldFacade

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the form editor (Facades API)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Configure the appearance for the field "Footer"
            editor.Facade.Font = Aspose.Pdf.Facades.FontStyle.TimesItalic;   // italic font (fully qualified)
            editor.Facade.BackgroundColor = System.Drawing.Color.LightGray; // light‑gray background (fully qualified)

            // Apply the configured appearance to the specific field
            editor.DecorateField("Footer");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field \"Footer\" styled and saved to '{outputPath}'.");
    }
}
