using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output1.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a FormEditor facade and bind the source PDF
        var formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        // Add a text field named "CustomerName" on page 1
        // AddField signature: AddField(FieldType fieldType, string name, int page, double llx, double lly, double urx, double ury)
        formEditor.AddField(FieldType.Text, "CustomerName", 1, 100, 150, 200, 170);

        // Save the modified PDF to the destination file
        formEditor.Save(outputPath);

        Console.WriteLine($"Form field added and saved to '{outputPath}'.");
    }
}
