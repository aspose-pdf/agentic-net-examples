using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Folder containing source PDF forms
        const string inputFolder = "InputPdfs";
        // Folder where exported XML files will be saved
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
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(outputFolder, fileNameWithoutExt + ".xml");

            try
            {
                // Load the PDF document
                using (Document doc = new Document(pdfPath))
                {
                    // Initialize a FormEditor instance (required by the task)
                    using (FormEditor editor = new FormEditor(doc))
                    {
                        // Export form data to XML using the Form facade
                        using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                        {
                            Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(doc);
                            form.ExportXml(xmlStream);
                        }
                    }
                }

                Console.WriteLine($"Exported XML: {xmlPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}