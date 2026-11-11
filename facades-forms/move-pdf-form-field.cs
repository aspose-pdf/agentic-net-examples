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
        const string fieldName = "DateField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and bind it to FormEditor
        using (Document doc = new Document(inputPath))
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(doc);

            // Desired position: lower‑left (100,200), width 150, height 20
            float llx = 100f;
            float lly = 200f;
            float urx = llx + 150f; // 250
            float ury = lly + 20f;  // 220

            // Move the field to the new rectangle
            editor.MoveField(fieldName, llx, lly, urx, ury);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' moved and saved to '{outputPath}'.");
    }
}