using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_print.pdf"; // PDF with PrintForm button

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
                // Coordinates: lower‑left (100,100), upper‑right (200,150)
                formEditor.AddField(FieldType.PushButton, "PrintForm", 1, 100, 100, 200, 150);

                // JavaScript to open the print dialog when the button is clicked
                string jsCode = "this.print({bUI:true,bSilent:false,bShrinkToFit:true});";

                // Attach the JavaScript to the button field
                formEditor.AddFieldScript("PrintForm", jsCode);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with PrintForm button: {outputPath}");
    }
}