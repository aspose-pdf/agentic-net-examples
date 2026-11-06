using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "justified_output.pdf";
        const string fieldName = "TextBox1";
        const string fieldValue = "This is a sample text that will be justified inside the textbox field.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ---------------------------------------------------------------------
        // 1. Set the visual alignment of the textbox field to Justify.
        // ---------------------------------------------------------------------
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);
            // SetFieldAlignment expects an int; cast from HorizontalAlignment enum.
            editor.SetFieldAlignment(fieldName, (int)Aspose.Pdf.HorizontalAlignment.Justify);
            editor.Save(outputPath);
        }

        // ---------------------------------------------------------------------
        // 2. Fill the textbox field with the desired text.
        // ---------------------------------------------------------------------
        using (Form form = new Form())
        {
            form.BindPdf(outputPath);
            form.FillField(fieldName, fieldValue);
            form.Save(outputPath);
        }

        Console.WriteLine($"Justified PDF saved to '{outputPath}'.");
    }
}
