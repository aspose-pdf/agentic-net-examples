using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "filled_form.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON data not found: {jsonPath}");
            return;
        }

        using (Document doc = new Document(templatePath))
        {
            using (Form form = new Form(doc))
            {
                using (FileStream jsonStream = File.OpenRead(jsonPath))
                {
                    form.ImportJson(jsonStream);
                }
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Form filled PDF saved to '{outputPath}'.");
    }
}
