using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source PDF forms
        const string inputFolder = "FormPdfs";
        // Directory where the exported XML files will be saved
        const string outputFolder = "ExportedXml";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Determine output XML file name (same base name as PDF)
                string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
                string xmlPath = Path.Combine(outputFolder, xmlFileName);

                // Use the Form facade to bind the PDF and export its form data to XML
                using (Form form = new Form())
                {
                    form.BindPdf(pdfPath);

                    using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                    {
                        form.ExportXml(xmlStream);
                    }
                }

                Console.WriteLine($"Exported form data: '{pdfPath}' -> '{xmlPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
