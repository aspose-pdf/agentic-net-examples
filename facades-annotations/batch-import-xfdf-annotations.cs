using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Directory containing PDFs and matching XFDF files (current directory)
        string[] pdfFiles = Directory.GetFiles(".", "*.pdf");

        foreach (string pdfPath in pdfFiles)
        {
            string baseName = Path.GetFileNameWithoutExtension(pdfPath);
            string xfdfPath = baseName + ".xfdf";

            if (File.Exists(xfdfPath))
            {
                try
                {
                    using (Document pdfDoc = new Document(pdfPath))
                    {
                        // Import annotations from the matching XFDF file
                        pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                        // Save the annotated PDF with a new simple filename
                        string outputFileName = baseName + "_annotated.pdf";
                        pdfDoc.Save(outputFileName);
                        Console.WriteLine($"Processed '{pdfPath}' with '{xfdfPath}' -> '{outputFileName}'");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"No matching XFDF for '{pdfPath}', skipping.");
            }
        }
    }
}