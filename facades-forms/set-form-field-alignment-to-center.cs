using System;
using System.IO;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Create a FormEditor and bind it to the loaded document
            Aspose.Pdf.Facades.FormEditor formEditor = new Aspose.Pdf.Facades.FormEditor();
            formEditor.BindPdf(doc);

            // Set the alignment of the field named "Address" to center.
            // SetFieldAlignment expects an int; cast the HorizontalAlignment enum to int.
            formEditor.SetFieldAlignment("Address", (int)Aspose.Pdf.HorizontalAlignment.Center);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field alignment set and saved to '{outputPath}'.");
    }
}