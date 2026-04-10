using System;
using System.IO;
using Aspose.Pdf.Facades;   // FormEditor, Form
using Aspose.Pdf;          // Document (accessed via FormEditor)

class Program
{
    static void Main()
    {
        // Input PDF files – adjust the paths as needed
        string[] pdfFiles = new string[]
        {
            "Form1.pdf",
            "Form2.pdf",
            "Form3.pdf"
        };

        // Output directory for XML files
        string outputDir = "ExportedXml";
        Directory.CreateDirectory(outputDir);

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Use FormEditor to load each PDF (lifecycle managed by using)
            using (FormEditor formEditor = new FormEditor())
            {
                formEditor.BindPdf(pdfPath);               // Load the PDF into the facade

                // The underlying Document can be accessed via the Document property
                Document doc = formEditor.Document;

                // Create a Form object based on the loaded document to export form data
                Form form = new Form(doc);

                // Prepare output XML file path (same name as PDF, .xml extension)
                string xmlFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".xml";
                string xmlPath = Path.Combine(outputDir, xmlFileName);

                // Export form fields to XML
                using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    form.ExportXml(xmlStream);
                }

                Console.WriteLine($"Exported form data from '{pdfPath}' to '{xmlPath}'.");
            }
        }
    }
}