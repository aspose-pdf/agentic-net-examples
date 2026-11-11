using System;
using System.IO;
using System.Drawing;
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

        // Load the PDF with FormEditor (Facades API)
        var editor = new FormEditor();
        editor.BindPdf(inputPath);

        // Define the appearance that will be applied to the field "CustomerName"
        editor.Facade = new FormFieldFacade
        {
            BorderColor = System.Drawing.Color.LightGray,
            BackgroundColor = System.Drawing.Color.LightYellow,
            BorderWidth = 1,
            BorderStyle = FormFieldFacade.BorderStyleSolid,
            Alignment = FormFieldFacade.AlignLeft,
            FontSize = 12,
            TextColor = System.Drawing.Color.FromArgb(0, 0, 128), // dark blue
            Font = Aspose.Pdf.Facades.FontStyle.Helvetica // fully‑qualified to avoid ambiguity
        };

        // Apply the appearance to the existing field
        editor.DecorateField("CustomerName");

        // Save the modified PDF
        editor.Save(outputPath);
        editor.Close();
    }
}
