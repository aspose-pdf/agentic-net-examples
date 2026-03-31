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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Form form = new Form(inputPath))
        {
            foreach (string fieldName in form.FieldNames)
            {
                if (fieldName.StartsWith("Old_"))
                {
                    string newFieldName = "New_" + fieldName.Substring(4);
                    form.RenameField(fieldName, newFieldName);
                }
            }

            form.Save(outputPath);
        }

        Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
    }
}
