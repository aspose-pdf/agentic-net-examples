using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string jsonPath = "data.json";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON not found: {jsonPath}");
            return;
        }

        // Bind the PDF to the Form facade
        using (Form form = new Form())
        {
            form.BindPdf(pdfPath);

            // Import JSON data; fields that do not exist in the PDF are ignored automatically
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                form.ImportJson(jsonStream);
            }

            // Save the updated PDF
            form.Save(outputPath);
        }

        Console.WriteLine($"Form data imported and saved to '{outputPath}'.");
    }
}