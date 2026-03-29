using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "TermsAccepted";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Form form = new Form(inputPath))
        {
            bool filled = form.FillField(fieldName, true);
            if (!filled)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or could not be filled.");
            }
            form.Save(outputPath);
        }

        Console.WriteLine($"Checkbox '{fieldName}' set to true and saved to '{outputPath}'.");
    }
}