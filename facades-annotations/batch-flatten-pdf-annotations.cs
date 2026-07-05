using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input folder can be passed as first argument; default to "InputPdfs"
        string inputFolder = args.Length > 0 ? args[0] : "InputPdfs";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            string directory = Path.GetDirectoryName(pdfPath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
            string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_flattened.pdf");

            try
            {
                // Create the facade, load the PDF, flatten annotations, and save
                PdfAnnotationEditor editor = new PdfAnnotationEditor();   // create
                editor.BindPdf(pdfPath);                                 // load
                editor.FlatteningAnnotations();                          // flatten all annotations
                editor.Save(outputPath);                                 // save
                editor.Close();                                          // release resources

                Console.WriteLine($"Flattened: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}