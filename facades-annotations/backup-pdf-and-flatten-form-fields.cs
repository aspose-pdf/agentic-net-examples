using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string backupPath = "input_backup.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Backup the original PDF before any flattening
                doc.Save(backupPath);

                // Flatten all form fields using the Form facade
                using (Form form = new Form(doc))
                {
                    form.FlattenAllFields();
                }

                // Save the flattened PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Backup created at '{backupPath}'.");
            Console.WriteLine($"Flattened PDF saved at '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}