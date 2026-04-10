using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Configure visual attributes for the field
                formEditor.Facade = new FormFieldFacade();
                formEditor.Facade.BackgroundColor = System.Drawing.Color.LightYellow; // background shade
                formEditor.Facade.TextColor       = System.Drawing.Color.DarkBlue;    // text color
                formEditor.Facade.BorderColor     = System.Drawing.Color.Green;        // border color
                // Use the FontStyle enum instead of a string
                formEditor.Facade.Font = Aspose.Pdf.Facades.FontStyle.Helvetica; // font name
                formEditor.Facade.FontSize = 14;                                 // font size

                // Apply the decoration to the specific field
                formEditor.DecorateField("CustomerName");

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Field 'CustomerName' decorated and saved to '{outputPath}'.");
    }
}
