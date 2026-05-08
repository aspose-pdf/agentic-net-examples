using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing the PDF forms
        const string inputFolder = "FormPdfs";
        // Output folder for the exported XML files
        const string outputFolder = "ExportedXml";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Get all PDF files in the input folder
        string[] pdfFiles = Directory.GetFiles(inputFolder, "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            // Determine the XML output file name (same base name, .xml extension)
            string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
            string xmlPath = Path.Combine(outputFolder, xmlFileName);

            // Use FormEditor to bind the PDF (required by the task)
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(pdfPath);

                // Create a Form instance from the bound document to export data
                using (Form form = new Form(editor.Document))
                {
                    // Export the form fields to an XML stream
                    using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                    {
                        form.ExportXml(xmlStream);
                    }
                }
            }

            Console.WriteLine($"Exported XML for '{Path.GetFileName(pdfPath)}' to '{xmlPath}'.");
        }
    }
}