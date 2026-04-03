using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputFolder  = "InputPdfs";
        const string outputFolder = "OutputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string outputPath = Path.Combine(outputFolder, Path.GetFileName(pdfPath));

            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF file
                editor.BindPdf(pdfPath);

                // Set a maximum of 100 characters for the field named "Comments"
                editor.SetFieldLimit("Comments", 100);

                // Save the modified PDF
                editor.Save(outputPath);
            }

            Console.WriteLine($"Processed: {pdfPath} -> {outputPath}");
        }
    }
}