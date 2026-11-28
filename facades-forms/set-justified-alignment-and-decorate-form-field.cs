using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "CommentsJustified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use a using‑statement so the FormEditor is disposed automatically
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF
            editor.BindPdf(inputPath);

            // Set justified alignment for the "Comments" field (alignment is an int)
            editor.SetFieldAlignment("Comments", (int)Aspose.Pdf.HorizontalAlignment.Justify);

            // Configure visual decoration via the Facade (System.Drawing.Color is required)
            editor.Facade.BorderColor = System.Drawing.Color.Black;
            editor.Facade.BackgroundColor = System.Drawing.Color.LightGray;
            editor.Facade.BorderWidth = 1F; // thin border, can also use FormFieldFacade.BorderWidthThin

            // Apply the decoration to the specific field
            editor.DecorateField("Comments");

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Saved justified form PDF to '{outputPath}'.");
    }
}
