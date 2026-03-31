using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        using (Document doc = new Document(inputPath))
        {
            Form form = doc.Form;
            if (form != null && form.Fields != null)
            {
                List<string> buttonFieldNames = new List<string>();
                foreach (Field field in form.Fields)
                {
                    if (field is ButtonField)
                    {
                        buttonFieldNames.Add(field.Name);
                    }
                }

                foreach (string name in buttonFieldNames)
                {
                    form.Delete(name);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Button fields removed. Saved to '{outputPath}'.");
    }
}