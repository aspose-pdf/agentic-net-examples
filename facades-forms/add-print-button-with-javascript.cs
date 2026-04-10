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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor on the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a push button named "PrintForm" on page 1
                // Rectangle coordinates: lower‑left (100, 500), upper‑right (200, 550)
                formEditor.AddField(FieldType.PushButton, "PrintForm", 1, 100, 500, 200, 550);

                // JavaScript that opens the print dialog
                string jsCode = "this.print({bUI:true,bSilent:false,bShrinkToFit:true});";

                // Attach the JavaScript to the button
                formEditor.AddFieldScript("PrintForm", jsCode);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with PrintForm button saved to '{outputPath}'.");
    }
}