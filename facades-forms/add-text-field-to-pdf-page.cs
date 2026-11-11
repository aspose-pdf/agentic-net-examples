using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "CustomerName";

        // Rectangle coordinates (lower‑left x,y and upper‑right x,y)
        const float llx = 100f; // adjust as needed
        const float lly = 500f;
        const float urx = 300f;
        const float ury = 550f;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor (Facades API) handles PDF form operations.
        using (FormEditor editor = new FormEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Add a text field named "CustomerName" on page 1.
            // FormEditor does not expose AddTextField; use AddField with FieldType.Text.
            editor.AddField(FieldType.Text, fieldName, 1, llx, lly, urx, ury);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text field '{fieldName}' added to page 1 and saved as '{outputPath}'.");
    }
}
