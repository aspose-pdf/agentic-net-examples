using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonPath = "data.json";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            Form form = new Form(inputPdf, outputPdf);
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportJson(jsonStream);
            }

            object[] importResults = form.ImportResult;
            if (importResults != null && importResults.Length > 0)
            {
                Console.WriteLine("Import results:");
                foreach (object result in importResults)
                {
                    Console.WriteLine(result.ToString());
                }
            }

            form.Save();
            Console.WriteLine($"Form data imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during import: {ex.Message}");
        }
    }
}
