using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_print_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor and bind the existing PDF
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        // Add a push button named "PrintForm" on page 1
        // Coordinates: lower‑left (100, 100), upper‑right (200, 150)
        formEditor.AddField(FieldType.PushButton, "PrintForm", 1, 100f, 100f, 200f, 150f);

        // JavaScript to open the print dialog
        string jsCode = "app.execMenuItem('Print');";

        // Attach the JavaScript to the newly created button
        formEditor.AddFieldScript("PrintForm", jsCode);

        // Save the modified PDF
        formEditor.Save(outputPath);
        formEditor.Close();

        Console.WriteLine($"PDF saved with PrintForm button: {outputPath}");
    }
}