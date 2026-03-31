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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a push button field named "PrintForm" on page 1
                formEditor.AddField(FieldType.PushButton, "PrintForm", 1, 100f, 100f, 200f, 150f);

                // Attach JavaScript to open the print dialog
                formEditor.AddFieldScript("PrintForm", "this.print(true);");

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF saved with print button: {outputPath}");
    }
}