using System;
using System.IO;
using Aspose.Pdf.Facades; // Only one import needed

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "decorated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the FormEditor (Facades API)
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // ----- Configure field appearance via the Facade -----
            // Custom border color (dark blue)
            editor.Facade.BorderColor = System.Drawing.Color.FromArgb(0, 51, 204);
            // Custom background shade (light yellow‑gray)
            editor.Facade.BackgroundColor = System.Drawing.Color.FromArgb(230, 230, 179);

            // Optional: set border width and style (thin solid line)
            editor.Facade.BorderWidth = FormFieldFacade.BorderWidthThin; // float constant
            editor.Facade.BorderStyle = FormFieldFacade.BorderStyleSolid; // int constant

            // Set font style to bold (using a built‑in Facade font style)
            editor.Facade.Font = FontStyle.TimesBold; // Aspose.Pdf.Facades.FontStyle enum
            editor.Facade.FontSize = 12F;

            // Apply the configured appearance to the specific field
            editor.DecorateField("CustomerName");

            // Save the modified PDF
            editor.Save(outputPath);
        }
    }
}