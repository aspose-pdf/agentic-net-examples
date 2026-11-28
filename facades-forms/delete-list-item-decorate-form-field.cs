using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "Colors.pdf";
        const string outputPath = "ColorsUpdated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF with FormEditor
        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPath);

        // Remove the list item "Red" from the list box named "Colors"
        editor.DelListItem("Colors", "Red");

        // Set visual appearance for the field using a facade
        editor.Facade = new FormFieldFacade
        {
            BorderColor = System.Drawing.Color.LightGray,
            BackgroundColor = System.Drawing.Color.FromArgb(
                (int)(0.9 * 255),
                (int)(0.9 * 255),
                (int)(0.95 * 255))
        };

        // Apply the appearance to the existing field
        editor.DecorateField("Colors");

        // Save the updated PDF
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
