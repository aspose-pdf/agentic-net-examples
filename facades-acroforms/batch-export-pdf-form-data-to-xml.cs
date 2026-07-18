using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input directory containing PDF forms
        const string inputDir = "InputPdfs";
        // Output directory for exported XML files
        const string outputDir = "ExportedXml";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Get all PDF files in the input directory
        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found to process.");
            return;
        }

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Determine output XML file path
                string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
                string xmlPath = Path.Combine(outputDir, xmlFileName);

                // Initialize FormEditor and bind the PDF
                using (FormEditor editor = new FormEditor())
                {
                    editor.BindPdf(pdfPath);

                    // Use Form facade to export form data to XML
                    using (Form form = new Form(editor.Document))
                    {
                        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                        {
                            form.ExportXml(xmlStream);
                        }
                    }
                }

                Console.WriteLine($"Exported XML for '{pdfPath}' to '{xmlPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}