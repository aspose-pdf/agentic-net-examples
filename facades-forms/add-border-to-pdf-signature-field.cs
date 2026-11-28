using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "SignatureBorder.pdf";
        const string signatureFieldName = "Signature1"; // adjust to actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF document to a FormEditor instance
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Prepare a facade that defines the visual appearance we want
            var facade = new FormFieldFacade
            {
                BorderColor = Color.Blue,               // System.Drawing.Color
                BorderWidth = 2,                        // thickness in points
                BorderStyle = FormFieldFacade.BorderStyleSolid,
                BackgroundColor = Color.LightGray      // optional decoration
            };

            // Assign the facade to the editor and apply it to the existing signature field
            editor.Facade = facade;
            editor.DecorateField(signatureFieldName);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
