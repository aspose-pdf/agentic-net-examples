using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "moved2.pdf";
        const string fieldName  = "LastName";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor works directly with the PDF file; no Document object is required.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF.
            formEditor.BindPdf(inputPath);

            // Move the specified field to the new rectangle.
            // FormEditor.MoveField does not have a Rectangle overload – it requires the four
            // coordinate values (llx, lly, urx, ury) as separate float arguments.
            formEditor.MoveField(fieldName, 120f, 170f, 220f, 270f);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' moved and saved to '{outputPath}'.");
    }
}
