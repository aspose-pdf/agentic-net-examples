using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "moved5.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to FormEditor
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Move the field named "Address" to the new rectangle (llx, lly, urx, ury)
            // FormEditor.MoveField(string fieldName, float llx, float lly, float urx, float ury)
            formEditor.MoveField("Address", 180f, 230f, 280f, 330f);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field 'Address' moved and saved to '{outputPath}'.");
    }
}
