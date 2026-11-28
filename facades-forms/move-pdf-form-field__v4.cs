using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "moved3.pdf";
        const string fieldName = "Email";

        // Verify the source file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use FormEditor (it implements IDisposable) to modify the form field.
        using (var formEditor = new FormEditor())
        {
            // Bind the PDF document.
            formEditor.BindPdf(inputPath);

            // Define the new rectangle for the field (llx, lly, urx, ury).
            var newRect = new Rectangle(140, 190, 240, 290);

            // MoveField expects float arguments. Cast the double values from Rectangle to float.
            formEditor.MoveField(
                fieldName,
                (float)newRect.LLX,
                (float)newRect.LLY,
                (float)newRect.URX,
                (float)newRect.URY);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' moved and saved to '{outputPath}'.");
    }
}
