using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "SummaryAdded.pdf";
        const string fieldName = "Summary";
        const string summaryText = "This is the summary text that will appear in the PDF.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // 1. Add a new text field on page 2 using FormEditor.
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Define rectangle coordinates as floats (the AddField overload expects float).
            float llx = 100f;
            float lly = 500f;
            float urx = 400f;
            float ury = 550f;

            // Add a single‑line text field.
            editor.AddField(FieldType.Text, fieldName, 2, llx, lly, urx, ury);

            // Save the PDF that now contains the empty field.
            editor.Save(outputPath);
        }

        // 2. Fill the newly added field with the summary text using the Form facade.
        using (Form form = new Form())
        {
            form.BindPdf(outputPath);
            form.FillField(fieldName, summaryText);
            form.Save(outputPath);
        }

        Console.WriteLine($"PDF with summary field saved to '{outputPath}'.");
    }
}
