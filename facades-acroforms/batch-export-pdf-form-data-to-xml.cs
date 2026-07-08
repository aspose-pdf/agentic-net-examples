using System;
using System.IO;
using Aspose.Pdf.Facades; // Contains FormEditor and Form classes

class Program
{
    static void Main()
    {
        // Folder containing source PDF forms
        const string inputFolder = "InputPdfs";
        // Folder where exported XML files will be saved
        const string outputFolder = "ExportedXml";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify that the input directory exists; create it if it does not.
        // This prevents a DirectoryNotFoundException when the folder is missing.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder '{inputFolder}' does not exist. Creating an empty folder.");
            Directory.CreateDirectory(inputFolder);
            // No PDFs to process, exit gracefully.
            return;
        }

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            // Derive XML file name from PDF file name
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string xmlPath = Path.Combine(outputFolder, fileNameWithoutExt + ".xml");

            // Use FormEditor to open the PDF (wrapped in using for deterministic disposal)
            using (FormEditor editor = new FormEditor())
            {
                // Bind the PDF document to the editor
                editor.BindPdf(pdfPath);

                // Export the form data to XML using the Form facade
                // Form can be constructed from the Document that FormEditor works on
                using (Form form = new Form(editor.Document))
                {
                    // Create the output XML file stream
                    using (FileStream xmlStream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                    {
                        // Export form fields (excluding button values) to the XML stream
                        form.ExportXml(xmlStream);
                    }
                }
            }

            Console.WriteLine($"Exported: {pdfPath} -> {xmlPath}");
        }
    }
}
