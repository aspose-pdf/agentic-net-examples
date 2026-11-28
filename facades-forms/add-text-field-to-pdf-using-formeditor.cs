using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output001.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Optional: define a default appearance for fields added after this point.
            editor.Facade = new FormFieldFacade
            {
                BorderColor = System.Drawing.Color.Black,
                BackgroundColor = System.Drawing.Color.White,
                TextColor = System.Drawing.Color.Black,
                BorderWidth = FormFieldFacade.BorderWidthThin,
                BorderStyle = FormFieldFacade.BorderStyleSolid,
                Alignment = FormFieldFacade.AlignLeft,
                Font = FontStyle.Helvetica,
                FontSize = 12F
            };

            // Add a text field named "CustomerName" on page 1.
            // Rectangle coordinates: lower‑left (100,200), upper‑right (300,220)
            editor.AddField(FieldType.Text, "CustomerName", 1, 100, 200, 300, 220);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with text field saved to '{outputPath}'.");
    }
}
