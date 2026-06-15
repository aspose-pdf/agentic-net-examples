using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a backup file name in the same folder
        string backupPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(inputPath) + "_backup.pdf");

        try
        {
            // Load the original PDF document
            using (Document doc = new Document(inputPath))
            {
                // Save a backup copy before any modifications
                doc.Save(backupPath); // PDF format is inferred; no SaveOptions needed

                // Use the Form facade to flatten all form fields
                Form formFacade = new Form(doc);
                formFacade.FlattenAllFields();

                // Save the flattened document to the desired output location
                doc.Save(outputPath);
            }

            Console.WriteLine($"Backup created at: {backupPath}");
            Console.WriteLine($"Flattened PDF saved at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}