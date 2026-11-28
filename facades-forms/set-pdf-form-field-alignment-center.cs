using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output8.pdf";
        const string fieldName = "Comments";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable – use a using block
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the PDF document to the editor
            formEditor.BindPdf(inputPath);

            // Set the alignment of the "Comments" field to center.
            // SetFieldAlignment expects an int, so cast the HorizontalAlignment enum.
            formEditor.SetFieldAlignment(fieldName, (int)HorizontalAlignment.Center);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' alignment set to Center. Saved to '{outputPath}'.");
    }
}
