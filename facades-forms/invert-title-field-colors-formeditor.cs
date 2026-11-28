using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color used by FormEditor.Facade
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "TitleInverted.pdf";
        const string fieldName = "Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF to FormEditor inside a using block to ensure proper disposal
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(inputPath);

                // Retrieve the current appearance of the field via the default appearance (fallback to white/black)
                // FormEditor.Facade works with System.Drawing.Color, not Aspose.Pdf.Color.
                // Since there is no direct getter for a specific field's colors in recent versions,
                // we start from a known default (white background, black text) and invert those.
                System.Drawing.Color currentBg = System.Drawing.Color.White;
                System.Drawing.Color currentTxt = System.Drawing.Color.Black;

                // Invert the colors
                System.Drawing.Color invertedBg = InvertColor(currentBg);
                System.Drawing.Color invertedTxt = InvertColor(currentTxt);

                // Configure the facade with the inverted colors and a decorative border
                formEditor.Facade.BackgroundColor = invertedBg;          // System.Drawing.Color
                formEditor.Facade.TextColor = invertedTxt;              // System.Drawing.Color
                formEditor.Facade.BorderColor = System.Drawing.Color.Blue; // System.Drawing.Color
                formEditor.Facade.BorderWidth = 1.5f;                  // float value (no constant needed)

                // Apply the decoration to the specific field
                formEditor.DecorateField(fieldName);

                // Save the modified PDF
                formEditor.Save(outputPath);
                Console.WriteLine($"Saved inverted title PDF to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to invert a System.Drawing.Color (RGB components are 0..255)
    static System.Drawing.Color InvertColor(System.Drawing.Color color)
    {
        return System.Drawing.Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
    }
}
