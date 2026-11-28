using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Result.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the Form facade to bind the PDF and fill each field with its own name.
        using (Form form = new Form())
        {
            form.BindPdf(inputPath);

            // The Form facade exposes a FieldNames collection.
            // To set a field's value use Form.FillField(name, value).
            foreach (string fieldName in form.FieldNames)
            {
                form.FillField(fieldName, fieldName);
            }

            // Save the updated PDF.
            form.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}
