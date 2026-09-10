using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for source PDF, JSON data to import and the resulting PDF
        const string srcPdfPath = "input.pdf";
        const string jsonDataPath = "data.json";
        const string outPdfPath = "output.pdf";

        // Verify that source files exist
        if (!File.Exists(srcPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {srcPdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Use the Form facade with the constructor that only takes the source PDF.
        // The Form class implements IDisposable, so we wrap it in a using block.
        using (Form form = new Form(srcPdfPath))
        {
            try
            {
                // Import field values from a JSON stream.
                using (FileStream jsonStream = File.OpenRead(jsonDataPath))
                {
                    form.ImportJson(jsonStream);
                }
            }
            // FormException no longer exists; catch generic Exception and extract the missing field name via reflection.
            catch (Exception ex)
            {
                var fieldProp = ex.GetType().GetProperty("FieldName");
                if (fieldProp != null)
                {
                    var missingField = fieldProp.GetValue(ex) as string;
                    Console.Error.WriteLine($"Missing form field: {missingField}");
                }
                else
                {
                    Console.Error.WriteLine($"Error during import: {ex.Message}");
                }
                return; // stop processing on error
            }

            // Save the updated PDF using the overload that accepts the destination path.
            form.Save(outPdfPath);
        }

        Console.WriteLine($"Form fields imported successfully. Output saved to '{outPdfPath}'.");
    }
}
