using System;
using System.IO;
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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor for the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a push button named "PrintForm" on page 1
                // Rectangle coordinates: lower‑left (100,100), upper‑right (200,150)
                formEditor.AddField(FieldType.PushButton, "PrintForm", 1, 100, 100, 200, 150);

                // Attach JavaScript that opens the print dialog
                string javaScript = "this.print(true);";
                formEditor.AddFieldScript("PrintForm", javaScript);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with PrintForm button saved to '{outputPath}'.");
    }
}