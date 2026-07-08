using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "filled.pdf";

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

        // Load the PDF form using the Form facade
        using (Form form = new Form(templatePath))
        {
            // Import JSON data into the form fields (keys must match full field names)
            using (FileStream jsonStream = File.OpenRead(jsonPath))
            {
                form.ImportJson(jsonStream);
            }

            // Save the filled PDF to the specified output file
            form.Save(outputPath);
        }

        Console.WriteLine($"Filled PDF saved to '{outputPath}'.");
    }
}