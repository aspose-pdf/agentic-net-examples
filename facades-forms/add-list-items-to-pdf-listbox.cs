using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "feedback_form_v1.pdf";
        const string outputPath = "quality_options.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            using (FormEditor editor = new FormEditor())
            {
                // Bind the existing PDF document to the FormEditor facade
                editor.BindPdf(doc);

                // Add the desired items to the existing ListBox named "Quality"
                string[] items = { "Good", "Average", "Poor" };
                foreach (var item in items)
                {
                    editor.AddListItem("Quality", item);
                }

                // Persist the changes to a new file
                editor.Save(outputPath);
            }

            Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
