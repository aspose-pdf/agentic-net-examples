using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "modified.pdf";
        const string sourceFieldName = "Email";
        const string copyFieldName = "EmailCopy";
        const int targetPage = 3;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -------------------------------------------------------------------
        // 1. Read the current value of the source field using the Form facade.
        // -------------------------------------------------------------------
        string emailValue;
        using (var form = new Form())
        {
            form.BindPdf(inputPath);
            // GetField returns the field's value as a string (or null if not present).
            emailValue = form.GetField(sourceFieldName) ?? string.Empty;
        }

        // -------------------------------------------------------------------
        // 2. Add a new text field on the target page with the same value.
        // -------------------------------------------------------------------
        // Define the rectangle where the new field will be placed.
        // Adjust the coordinates (llx, lly, urx, ury) to suit your layout.
        var fieldRect = new Rectangle(100, 500, 300, 520);

        using (var editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // AddField overload expects float values for the coordinates.
            // Cast the double values from Rectangle to float.
            editor.AddField(
                FieldType.Text,
                copyFieldName,
                emailValue,
                targetPage,
                (float)fieldRect.LLX,
                (float)fieldRect.LLY,
                (float)fieldRect.URX,
                (float)fieldRect.URY);

            // Save the modified document.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{sourceFieldName}' copied to '{copyFieldName}' on page {targetPage}.");
        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}
