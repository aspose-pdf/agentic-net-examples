using System;
using System.IO;
using System.Drawing; // for System.Drawing.Color
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

        // Load the PDF document first, then pass the Document instance to FormEditor.
        Document pdfDoc = new Document(inputPath);

        using (FormEditor editor = new FormEditor(pdfDoc))
        {
            // Configure visual appearance for the field.
            editor.Facade = new FormFieldFacade();
            // The Facade properties expect System.Drawing.Color, so use fully‑qualified System.Drawing.Color values.
            editor.Facade.BorderColor     = System.Drawing.Color.Green;        // custom border color
            editor.Facade.BackgroundColor = System.Drawing.Color.LightYellow; // background shade
            editor.Facade.TextColor       = System.Drawing.Color.DarkBlue;    // text color
            editor.Facade.Font            = Aspose.Pdf.Facades.FontStyle.HelveticaBold; // font style
            editor.Facade.FontSize        = 14;                                   // font size

            // Apply the appearance settings to the specific field.
            editor.DecorateField("CustomerName");

            // Save the result.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field 'CustomerName' decorated and saved to '{outputPath}'.");
    }
}
