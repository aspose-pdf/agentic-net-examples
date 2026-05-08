using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Directory to process – passed as first argument or current directory if none provided
        string rootPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Find all PDF files recursively
        string[] pdfFiles = Directory.GetFiles(rootPath, "*.pdf", SearchOption.AllDirectories);

        foreach (string pdfPath in pdfFiles)
        {
            try
            {
                // Initialize the facade and bind the PDF file
                PdfFileSignature pdfSign = new PdfFileSignature();
                pdfSign.BindPdf(pdfPath);

                // Remove every signature from the document
                pdfSign.RemoveSignatures();

                // Save the result – create a new file to avoid overwriting the original
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath),
                    Path.GetFileNameWithoutExtension(pdfPath) + "_unsigned.pdf");

                pdfSign.Save(outputPath);

                // Release resources held by the facade
                pdfSign.Close();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }

        Console.WriteLine("All signatures have been removed.");
    }
}