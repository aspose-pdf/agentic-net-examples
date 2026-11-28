using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "output009.pdf";
        const string fieldName = "OrderID";
        const int pageNumber = 1;
        const int charLimit = 10;

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Create a FormEditor instance and bind the existing PDF.
        var editor = new FormEditor();
        editor.BindPdf(sourcePath);

        // Define the rectangle for the new text field (lower‑left x,y and upper‑right x,y).
        // Placed at (100,100) with a width of 200 and height of 20.
        float llx = 100f;
        float lly = 100f;
        float urx = 300f; // llx + width (200)
        float ury = 120f; // lly + height (20)

        // Add a text field of type FieldType.Text.
        editor.AddField(FieldType.Text, fieldName, pageNumber, llx, lly, urx, ury);

        // Set the maximum number of characters allowed in the field.
        editor.SetFieldLimit(fieldName, charLimit);

        // Save the modified PDF and release resources.
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"PDF saved as '{outputPath}'.");
    }
}
