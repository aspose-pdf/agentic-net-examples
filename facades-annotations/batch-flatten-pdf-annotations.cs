using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process: use first argument or current directory
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Process each PDF file in the folder
        foreach (string pdfFile in Directory.GetFiles(folderPath, "*.pdf"))
        {
            try
            {
                // Build the output file name with "_flattened" suffix
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfFile);
                string outputFile = Path.Combine(folderPath, $"{fileNameWithoutExt}_flattened.pdf");

                // Create the annotation editor, bind the source PDF, flatten annotations, and save
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(pdfFile);                     // Load the PDF into the editor
                editor.FlatteningAnnotations();              // Flatten all annotations
                editor.Save(outputFile);                     // Save the flattened PDF
                editor.Close();                              // Release resources held by the editor

                Console.WriteLine($"Flattened: '{pdfFile}' → '{outputFile}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfFile}': {ex.Message}");
            }
        }
    }
}