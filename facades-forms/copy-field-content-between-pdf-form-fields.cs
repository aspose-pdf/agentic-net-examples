using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the Form facade for reading and writing field values.
        using (Form form = new Form())
        {
            form.BindPdf(inputPath);

            // Read the value from the source field.
            string sourceValue = form.GetField("SourceNotes");

            // Write the same value into the target field.
            form.FillField("TargetNotes", sourceValue);

            // Save the modified PDF.
            form.Save(outputPath);
        }

        Console.WriteLine($"Copied content from 'SourceNotes' to 'TargetNotes' and saved as '{outputPath}'.");
    }
}
